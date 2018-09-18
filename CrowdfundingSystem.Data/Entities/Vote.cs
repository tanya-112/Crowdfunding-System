using CrowdfundingSystem.Data.Interfaces;

namespace CrowdfundingSystem.Data.Entities
{
    public abstract class Vote 
    {
        public int Id { get; set; }

        public string VoterId { get; set; }
        public User Voter { get; set; }

        public int IdeaId { get; set; }
        public Idea Idea { get; set; }
    }
}
