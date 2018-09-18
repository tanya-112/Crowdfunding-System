using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrowdfundingSystem.Data.ResourceModels.User
{
    public class UsersGetAllResourceModel
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        //public Address Address { get; set; }

        public int? ProfilePhotoId { get; set; }
        public Photo ProfilePhoto { get; set; }

        public string Bio { get; set; }

        public ICollection<UserInterest> Interests { get; set; }
    }
}
