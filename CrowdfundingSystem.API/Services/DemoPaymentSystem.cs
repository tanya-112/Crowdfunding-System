using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services
{
    public class DemoPaymentSystem : IPaymentSystem
    {
        public void ProcessPayment(IUnitOfWork unitOfWork, BankAccount senderBankAccount, BankAccount receiverBankAccount, decimal sumBeingTransferred)
        {
            senderBankAccount.SumAvailable = senderBankAccount.SumAvailable - sumBeingTransferred;
            unitOfWork.BankAccountRepository.Update(senderBankAccount);

            receiverBankAccount.SumAvailable = receiverBankAccount.SumAvailable + sumBeingTransferred;
            unitOfWork.BankAccountRepository.Update(receiverBankAccount);
        }
    }
}
