using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdfundingSystem.Data.ResourceModels.Vote
{
    public class PositiveVotePostResourceModel
    {
        public string VoterId { get; set; }
        public int IdeaId { get; set; } 
    }
}
