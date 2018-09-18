using System.ComponentModel.DataAnnotations;

namespace CrowdfundingSystem.Data.Entities
{
    //[Owned]
    public class Money
    {
        public double Sum { get; set; }

        [Required]
        public Currency Currency { get; set; }
    }
}
