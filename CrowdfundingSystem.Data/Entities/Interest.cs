using CrowdfundingSystem.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CrowdfundingSystem.Data.Entities
{
    public enum Interest
    {
        Books,
        Sports,
        Movies,
        IT,
        Art,
        Fashion
            //Add some more
    }

    public class UserInterest 
    {
        public int Id { get; set; }

        [Required]
        public Interest Interest { get; set; }
    }
}
