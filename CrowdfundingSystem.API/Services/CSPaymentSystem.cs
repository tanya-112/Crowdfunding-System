using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services
{
    public class CSPaymentSystem : ICSPaymentSystem
    {

        private readonly IPaymentSystem paymentSystem;
        public CSPaymentSystem(IPaymentSystem paymentSystem)
        {
            this.paymentSystem = paymentSystem;
        }
        public void ProcessPaymentWhileInCollectingMoneyState(IUnitOfWork unitOfWork,
            BankAccount userBankAccount, BankAccount CSOwnerBankAccount, decimal sumBeingTransferred,
            InnerMoneyAccount ideaInnerMoneyAccount)
        {
            // УЧЕСТЬ ПРОЦЕНТЫ!
            paymentSystem.ProcessPayment(unitOfWork, userBankAccount, CSOwnerBankAccount, sumBeingTransferred);
            ideaInnerMoneyAccount.SumAvailable += sumBeingTransferred;
        }
        public void RollbackPaymentWhileInCollectingMoneyState(IUnitOfWork unitOfWork,
            BankAccount CSOwnerBankAccount, BankAccount userBankAccount, decimal sumBeingTransferred,
            InnerMoneyAccount ideaInnerMoneyAccount)
        {
            // УЧЕСТЬ ПРОЦЕНТЫ!
            paymentSystem.ProcessPayment(unitOfWork, userBankAccount, CSOwnerBankAccount, sumBeingTransferred);
            ideaInnerMoneyAccount.SumAvailable -= sumBeingTransferred;
        }

        public void TransferMoneyToIdeaRealAccountAfterSuccesfullCollecting(IUnitOfWork unitOfWork,
    BankAccount CSOwnerBankAccount, BankAccount ideaBankAccount, decimal sumBeingTransferred,
    InnerMoneyAccount ideaInnerMoneyAccount)
        {
            // УЧЕСТЬ ПРОЦЕНТЫ!
            paymentSystem.ProcessPayment(unitOfWork, CSOwnerBankAccount, ideaBankAccount, sumBeingTransferred);
            ideaInnerMoneyAccount.SumTransferredToRealBankAccount = true;
        }

        public void ReturnMoneyToContributors(IUnitOfWork unitOfWork,
            BankAccount senderBankAccount, IEnumerable<BankAccount> receiversBankAccounts,
            InnerMoneyAccount ideaInnerMoneyAccount, Idea idea)
        {
            // УЧЕСТЬ ПРОЦЕНТЫ!
            foreach (var receiverBankAccount in receiversBankAccounts)
            {
                var sumBeingTransferred = unitOfWork.MoneyTransferRepository.Get(mt => (mt.IdeaId == idea.Id && mt.SenderAccountId == receiverBankAccount.Id)).FirstOrDefault().TransferredSum;
                paymentSystem.ProcessPayment(unitOfWork, senderBankAccount, receiverBankAccount, sumBeingTransferred);
            }
            ideaInnerMoneyAccount.SumAvailable = 0;
        }
    }
}
