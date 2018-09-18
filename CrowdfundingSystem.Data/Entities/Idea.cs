using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrowdfundingSystem.Data.Entities
{
    public class Idea
    {
        public Idea()
        {
            MoneyTransfers = new HashSet<MoneyTransfer>();
            PositiveVotes = new HashSet<PositiveVote>();
            NegativeVotes = new HashSet<NegativeVote>();
            Photos = new HashSet<Photo>();
            Videos = new HashSet<Video>();
            Audios = new HashSet<Audio>();
            DocumentsAboutTheIdea = new HashSet<Document>();
        }
        public int Id { get; set; }

        [Required]
        public string IdeaName { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        [Required]
        public string Description { get; set; }

        //public Money SumRequired { get; set; }
        public decimal SumRequired { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount BankAccountForTransfers { get; set; }

        public InnerMoneyAccount InnerAccountForTransfers { get; set; }

        public int? MainPhotoId { get; set; }
        public Photo MainPhoto { get; set; }

        public int? MainVideoId { get; set; }
        public Video MainVideo { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<Audio> Audios { get; set; }
        public ICollection<Document> DocumentsAboutTheIdea { get; set; }

        public DateTime DateOfPublishing { get; set; }
        public DateTime DeadlineForMoneyCollecting { get; set; }
        public bool IsStillCollectingMoney { get; set; }
        public bool CollectedTheMoney { get; set; }
        public DateTime? DateOfGoalAchieving { get; set; }
        public ICollection<PositiveVote> PositiveVotes { get; set; }
        public ICollection<NegativeVote> NegativeVotes { get; set; }
        public ICollection<MoneyTransfer> MoneyTransfers { get; set; }
    }
}
