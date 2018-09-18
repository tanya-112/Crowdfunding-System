using CrowdfundingSystem.API.Services.Interfaces;
using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services
{
    public class DeleteIdeaService : IDeleteIdeaService // maybe change the name to "deactivate" or "freeze" or something similar
    {
        private readonly ICSPaymentSystem csPaymentSystem;
        public DeleteIdeaService(ICSPaymentSystem csPaymentSystem)
        {
            this.csPaymentSystem = csPaymentSystem;
        }
        public bool DeleteIdea(IUnitOfWork unitOfWork, int id)
        {
                var ideaToDelete = unitOfWork.IdeaRepository.GetById(id, includeProperties: "MoneyTransfers,InnerAccountForTransfers");
                if (ideaToDelete.MoneyTransfers.Any())
                {
                    var csOwnerBankAccount = unitOfWork.CSOwnerRepository.Get(includeProperties: "BankAccounts").FirstOrDefault().BankAccounts.FirstOrDefault();
                    var contributorsBankAccounts = unitOfWork.MoneyTransferRepository.Get(filter: mt => mt.IdeaId == id, includeProperties: "SenderAccount")
                        .Select(mt => mt.SenderAccount);

                    csPaymentSystem.ReturnMoneyToContributors(unitOfWork, csOwnerBankAccount, contributorsBankAccounts,
                        ideaToDelete.InnerAccountForTransfers, ideaToDelete);
                }
                var deletionSuccesed = unitOfWork.IdeaRepository.Delete(id);
                if (deletionSuccesed)
                    return true;
                else
                    return false;
        }
    }
}
