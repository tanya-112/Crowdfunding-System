using CrowdfundingSystem.Data.Interfaces;
using System;

namespace CrowdfundingSystem.Data.Entities
{
    public class MoneyTransfer // represents a money transfer only from a user to idea
    {
        public int Id { get; set; }

        public int IdeaId { get; set; }
        public Idea Idea { get; set; }

        public int SenderAccountId { get; set; }       
        public BankAccount SenderAccount { get; set; }

        //public int SenderInnerAccountId { get; set; }
        //public InnerMoneyAccount SenderInnerAccount { get; set; }

        //public Money TransferredSum { get; set; }
        public decimal TransferredSum { get; set; }
        public DateTime DateOfTransfer { get; set; }
    }
}
