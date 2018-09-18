using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services
{
    public interface ICSPaymentSystem
    {
        void ProcessPaymentWhileInCollectingMoneyState(IUnitOfWork unitOfWork,
           BankAccount userBankAccount, BankAccount CSOwnerBankAccount, decimal sumBeingTransferred,
           InnerMoneyAccount ideaInnerMoneyAccount);
        void RollbackPaymentWhileInCollectingMoneyState(IUnitOfWork unitOfWork, BankAccount CSOwnerBankAccount,
           BankAccount userBankAccount, decimal sumBeingTransferred, InnerMoneyAccount ideaInnerMoneyAccount);

        void TransferMoneyToIdeaRealAccountAfterSuccesfullCollecting(IUnitOfWork unitOfWork, BankAccount CSOwnerBankAccount,
            BankAccount ideaBankAccount, decimal sumBeingTransferred, InnerMoneyAccount ideaInnerMoneyAccount);


        void ReturnMoneyToContributors(IUnitOfWork unitOfWork, BankAccount senderBankAccount, IEnumerable<BankAccount> receiversBankAccounts,
            InnerMoneyAccount ideaInnerMoneyAccount, Idea idea);

    }
}
