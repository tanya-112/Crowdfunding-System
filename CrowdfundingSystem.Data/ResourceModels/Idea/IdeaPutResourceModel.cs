using CrowdfundingSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrowdfundingSystem.Data.ResourceModels.Idea
{
    public class IdeaPutResourceModel 
    {

        public IFormFile MainPhoto { get; set; }

        public IFormFile MainVideo { get; set; }

        public IFormFileCollection Photos { get; set; }
        public IFormFileCollection Videos { get; set; }
        public IFormFileCollection Audios { get; set; }
        public IFormFileCollection Documents { get; set; }
        //[FileExtensions(Extensions = "jpg,jpeg")]
        //public IFormFileCollection MediaFiles { get; set; }
    }
}
