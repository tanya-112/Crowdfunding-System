using CrowdfundingSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrowdfundingSystem.Data.ResourceModels.User
{
    public class UserPostResourceModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        //public int? ProfilePhotoId { get; set; }
        //public Photo ProfilePhoto { get; set; }
        public IFormFile ProfilePhoto { get; set; }

        public string Bio { get; set; }

        public ICollection<UserInterest> Interests { get; set; }
        //public ICollection<BankAccount> BankAccounts { get; set; } // isn't needed in current version; might be used in the next ones
    }
}
