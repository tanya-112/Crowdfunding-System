using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdfundingSystem.Data.Entities
{
    public abstract class MoneyAccount
    {
        public int Id { get; set; }
        public decimal SumAvailable { get; set; } //is being used while the system works with a demo-version payment system, 
                                                  //with a real one - only InnerMoneyAccount will need it
    }
}
