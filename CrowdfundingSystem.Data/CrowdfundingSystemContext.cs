using CrowdfundingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CrowdfundingSystem.Data
{
    public class CrowdfundingSystemContext : IdentityDbContext<User>
    {
        public CrowdfundingSystemContext(DbContextOptions<CrowdfundingSystemContext> options) : base(options)
        {

        }

        public DbSet<CrowdfundingSystemOwner> CrowdfundingSystemOwners { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        //public DbSet<UserInterest> Interests { get; set; }
        //public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<InnerMoneyAccount> InnerMoneyAccounts { get; set; }
        //public DbSet<BankAccountCurrency> Currencies { get; set; }
        public DbSet<MoneyTransfer> MoneyTransfers { get; set; }
        public DbSet<PositiveVote> PositiveVotes { get; set; }
        public DbSet<NegativeVote> NegativeVotes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<Document> Documents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var idea = modelBuilder.Entity<Idea>();
            idea.HasOne(i => i.Owner).WithMany(o => o.UserIdeas).OnDelete(DeleteBehavior.Restrict);
            idea.HasOne(i => i.BankAccountForTransfers).WithMany().OnDelete(DeleteBehavior.Restrict);
            //idea.HasOne(i => i.MainPhoto).WithOne().OnDelete(DeleteBehavior.Cascade);
            //idea.HasOne(i=>i.MainVideo).WithOne().OnDelete(DeleteBehavior.Cascade);
            //idea.HasMany(i => i.Photos).WithOne().OnDelete(DeleteBehavior.Cascade);
            //idea.HasMany(i => i.Videos).WithOne().OnDelete(DeleteBehavior.Cascade);
            //idea.HasMany(i => i.DocumentsAboutTheIdea).WithOne().OnDelete(DeleteBehavior.Cascade);


            var innerMoneyAccount = modelBuilder.Entity<InnerMoneyAccount>();
            innerMoneyAccount.HasOne(a => a.ForIdea).WithOne(i => i.InnerAccountForTransfers).OnDelete(DeleteBehavior.Cascade);

            var moneyTransfer = modelBuilder.Entity<MoneyTransfer>();
            moneyTransfer.HasOne(mt => mt.SenderAccount).WithMany().OnDelete(DeleteBehavior.Restrict);
            moneyTransfer.HasOne(mt => mt.Idea).WithMany(i => i.MoneyTransfers).OnDelete(DeleteBehavior.Cascade);

            var bankAccount = modelBuilder.Entity<BankAccount>();
            //bankAccount.HasOne(a => a.AccountOwner).WithMany(o => o.BankAccounts).OnDelete(DeleteBehavior.Restrict);//if the user deletes his account we will assign the AccountOwner property to an "Anonymous(OrDeleted) user"
            //bankAccount.HasOne(a => a.Bank).WithMany().OnDelete(DeleteBehavior.Restrict);
            //bankAccount.HasOne(a => a.BankAccountCurrency).WithMany().OnDelete(DeleteBehavior.Restrict);

            var positiveVote = modelBuilder.Entity<PositiveVote>();
            positiveVote.HasOne(v => v.Idea).WithMany(i => i.PositiveVotes).OnDelete(DeleteBehavior.Cascade);
            positiveVote.HasOne(v => v.Voter).WithMany(v => v.PositiveVotesFromUser).OnDelete(DeleteBehavior.Restrict);//if the user deletes his account we will assign the Voter property to an "Anonymous(OrDeleted) user"

            var negativeVote = modelBuilder.Entity<NegativeVote>();
            negativeVote.HasOne(v => v.Idea).WithMany(i => i.NegativeVotes).OnDelete(DeleteBehavior.Cascade);
            negativeVote.HasOne(v => v.Voter).WithMany(v => v.NegativeVotesFromUser).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
