using CrowdfundingSystem.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CrowdfundingSystem.Data.Entities
{
    public class Bank 
    {
        public int Id { get; set; }

        [Required]
        public string BankName { get; set; }
    }
}