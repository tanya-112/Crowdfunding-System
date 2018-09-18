using CrowdfundingSystem.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CrowdfundingSystem.Data.Entities
{
    public abstract class Media 
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
