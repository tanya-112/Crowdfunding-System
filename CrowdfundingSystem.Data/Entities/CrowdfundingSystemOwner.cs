using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdfundingSystem.Data.Entities
{
    public class CrowdfundingSystemOwner : ICrowdfundingSystemMember
    {
        public int Id { get; set; }
        public ICollection<MoneyTransfer> MoneyTransfersFromOwner { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }

    }
}
