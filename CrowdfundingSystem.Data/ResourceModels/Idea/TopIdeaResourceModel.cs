using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdfundingSystem.Data.ResourceModels
{

    public class TopIdeaResourceModel
    {
        public int IdeaId { get; set; }
        public string IdeaName { get; set; }
        public string OwnerId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public decimal SumRequired { get; set; }
        public decimal TotalDonations { get; set; }
        public int TotalLikes { get; set; }
        public string MainPhotoUrl { get; set; }
        public string DeadlineForMoneyCollecting { get; set; }
    }
}
