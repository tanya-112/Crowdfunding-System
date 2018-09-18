using CrowdfundingSystem.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CrowdfundingSystem.Data.Entities
{
    public class BankAccount : MoneyAccount
    {
        [Required] //was not required
        public string AccountOwnerId { get; set; }
        //public CrowdfundingSystemMember AccountOwner { get; }

        //public int BankId { get; set; }
        //public Bank Bank { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        //public int BankAccountCurrencyId { get; set; }
        //public BankAccountCurrency BankAccountCurrency { get; set; } // isn't needed in current version; might be used in the next ones
    }
}
