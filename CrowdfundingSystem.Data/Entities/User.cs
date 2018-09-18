using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CrowdfundingSystem.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CrowdfundingSystem.Data.Entities
{
    public class User : IdentityUser, ICrowdfundingSystemMember
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //[Required]
        //public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        //public Address Address { get; set; }

        public long? FacebookId{ get; set; }
        public int? ProfilePhotoId { get; set; }
        public Photo ProfilePhoto { get; set; }

        public string Bio { get; set; }
        //public string Password { get; set; }

        public bool AccountIsDeleted { get; set; }
        public ICollection<Idea> UserIdeas { get; set; }
        public ICollection<PositiveVote> PositiveVotesFromUser { get; set; }
        public ICollection<NegativeVote> NegativeVotesFromUser { get; set; }
        public ICollection<MoneyTransfer> MoneyTransfersFromUser { get; set; }
        public ICollection<UserInterest> Interests { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; } 

    }
}
