using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrowdfundingSystem.Data.ResourceModels
{
    public class IdeaGetResourceModel
    {
        public int IdeaId { get; set; }
        public string IdeaName { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public decimal SumRequired { get; set; }
        public string AccountNumber { get; set; }
        public decimal TotalDonations { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        //public IEnumerable<PositiveVote> PositiveVotes { get; set; }
        //public IEnumerable<NegativeVote> NegativeVotes { get; set; }

        public string MainPhotoUrl { get; set; }
        public string MainVideoUrl { get; set; }

        public string DeadlineForMoneyCollecting { get; set; }
        public bool SuccededInCollectingTheMoney { get; set; }
        public bool IsStillCollectingMoney { get; set; }
        public string DateOfGoalAchieving { get; set; }
        public IEnumerable<string> PhotosUrls { get; set; }
        public IEnumerable<string> VideosUrls { get; set; }
        public IEnumerable<string> AudiosUrls { get; set; }
        public IEnumerable<string> DocumentsUrls { get; set; }

    }
}
