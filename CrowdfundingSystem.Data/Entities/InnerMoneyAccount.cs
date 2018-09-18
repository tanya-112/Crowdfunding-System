using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CrowdfundingSystem.Data.Entities
{
    public class InnerMoneyAccount : MoneyAccount
    {
        public int? IdeaId { get; set; }
        public Idea ForIdea { get; set; }

        public bool SumTransferredToRealBankAccount { get; set; }
    }
}
