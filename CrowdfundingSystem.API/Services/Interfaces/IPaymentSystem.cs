using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingSystem.Data.Interfaces;

namespace CrowdfundingSystem.API.Services
{
    public interface IPaymentSystem
    {
        void ProcessPayment(IUnitOfWork unitOfWork, BankAccount senderBankAccount, BankAccount receiverBankAccount, decimal sumBeingTransferred);
    }
}
