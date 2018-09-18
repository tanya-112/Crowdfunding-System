using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrowdfundingSystem.Data;
using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.ResourceModels;
using CrowdfundingSystem.API.Services;

namespace CrowdfundingSystem.API.Controllers
{
    [Produces("application/json")]
    [Route("api/MoneyTransfers")]
    public class MoneyTransfersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICSPaymentSystem csPaymentSystem;

        public MoneyTransfersController(IUnitOfWork unitOfWork, ICSPaymentSystem csPaymentSystem)
        {
            this.unitOfWork = unitOfWork;
            this.csPaymentSystem = csPaymentSystem;
        }

        // GET: api/MoneyTransfers
        [HttpGet]
        public async Task<IEnumerable<MoneyTransfer>> GetMoneyTransfers()
        {
            return await unitOfWork.MoneyTransferRepository.GetAsync();
        }

        // GET: api/MoneyTransfers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoneyTransfer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var moneyTransfer = await unitOfWork.MoneyTransferRepository.GetByIdAsync(id);

            if (moneyTransfer == null)
            {
                return NotFound();
            }

            return Ok(moneyTransfer);
        }

        // POST: api/MoneyTransfers
        [HttpPost]
        public IActionResult PostMoneyTransfer([FromBody] MoneyTransferPostResourceModel moneyTransfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var moneyTransferToInsert = new MoneyTransfer
            {
                IdeaId = moneyTransfer.IdeaId,
                SenderAccountId = moneyTransfer.SenderAccountId,
                TransferredSum = moneyTransfer.TransferredSum,
                DateOfTransfer = DateTime.Now
            };
            unitOfWork.MoneyTransferRepository.Insert(moneyTransferToInsert);

            var ideaInnerMoneyAccount = unitOfWork.IdeaRepository.Get(filter: i => i.Id == moneyTransferToInsert.IdeaId, includeProperties: "InnerAccountForTransfers")
                .FirstOrDefault()?.InnerAccountForTransfers;

            var senderAccount = unitOfWork.BankAccountRepository.Get(a => a.Id == moneyTransferToInsert.SenderAccountId).FirstOrDefault();
            var csOwnerId = unitOfWork.CSOwnerRepository.Get().FirstOrDefault().Id;
            var receiver = unitOfWork.BankAccountRepository.Get(filter: a => a.AccountOwnerId == csOwnerId.ToString()).FirstOrDefault();
            csPaymentSystem.ProcessPaymentWhileInCollectingMoneyState(unitOfWork, senderAccount,
                receiver, moneyTransferToInsert.TransferredSum, ideaInnerMoneyAccount);

            unitOfWork.Complete();

            return CreatedAtAction("GetMoneyTransfer", new { id = moneyTransferToInsert.Id }, moneyTransferToInsert);
        }


        // The business-rules do not allow deleting (i.e. reverting) a money transfer (i.e. cancelling the donation after it's been made)

        //// DELETE: api/MoneyTransfers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMoneyTransfer([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var moneyTransferToDelete = await unitOfWork.MoneyTransferRepository.GetByIdAsync(id, includeProperties: "SenderAccount");
        //    if(moneyTransferToDelete == null)
        //    {
        //        return NotFound();
        //    }

        //    var ideaInnerMoneyAccount = unitOfWork.IdeaRepository.Get(filter: i => i.Id == moneyTransferToDelete.IdeaId, includeProperties:"InnerAccountForTransfers").FirstOrDefault().InnerAccountForTransfers;

        //    csPaymentSystem.RollbackPaymentWhileInCollectingMoneyState(unitOfWork, unitOfWork.CSOwnerRepository.Get(includeProperties: "BankAccounts").SingleOrDefault().BankAccounts.FirstOrDefault(),
        //       moneyTransferToDelete.SenderAccount, moneyTransferToDelete.TransferredSum, ideaInnerMoneyAccount);


        //    bool deletionSuccesed = await unitOfWork.MoneyTransferRepository.DeleteAsync(id);

        //    await unitOfWork.CompleteAsync();
        //    return Ok(moneyTransferToDelete);
        //}

    }
}