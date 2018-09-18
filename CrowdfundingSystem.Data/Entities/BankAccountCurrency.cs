using CrowdfundingSystem.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CrowdfundingSystem.Data.Entities
{
    public enum Currency
    {
        USD,
        EUR,
        UAH,
        RUB
            //Add some more
    }

    public class BankAccountCurrency
    {
        public int Id { get; set; }

        [Required]
        public Currency Currency { get; set; }
    }
}
