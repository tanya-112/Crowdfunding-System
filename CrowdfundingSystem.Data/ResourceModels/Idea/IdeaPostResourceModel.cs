using CrowdfundingSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrowdfundingSystem.Data.ResourceModels
{
    public class IdeaPostResourceModel
    {
        [Required]
        public string IdeaName { get; set; }

        public string OwnerId { get; set; } // set it automatically after enabling authentication!

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal SumRequired { get; set; }

        [Required]
        //public int BankAccountId { get; set; }
        public string BankAccountNumber { get; set; }

        [Required]
        public string DeadlineForMoneyCollecting { get; set; }

        public IFormFile MainPhoto { get; set; }
        public IFormFile MainVideo { get; set; }

        public IFormFileCollection Photos { get; set; }
        public IFormFileCollection Videos { get; set; }
        public IFormFileCollection Audios { get; set; }
        public IFormFileCollection Documents { get; set; }
    }
}
