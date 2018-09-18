using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.Repositories;
using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CrowdfundingSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    //, IDisposable
    {
        private CrowdfundingSystemContext context;
        public UnitOfWork(CrowdfundingSystemContext context)
        {
            this.context = context ?? throw new ArgumentNullException("Context was not supplied");

            IdeaRepository = new IdeaRepository(context);
            UserRepository = new UserRepository(context);
            MoneyTransferRepository = new MoneyTransferRepository(context);
            CSOwnerRepository = new CSOwnerRepository(context);
            InnerMoneyAccountRepository = new GenericRepository<InnerMoneyAccount>(context);
            BankAccountRepository = new GenericRepository<BankAccount>(context);
            PhotoRepository = new GenericRepository<Photo>(context);
            PositiveVoteRepository = new GenericRepository<PositiveVote>(context);
            NegativeVoteRepository = new GenericRepository<NegativeVote>(context);
        }

        public IIdeaRepository IdeaRepository { get; }
        public ICSOwnerRepository CSOwnerRepository { get; }
        public IUserRepository UserRepository { get; }
        public IMoneyTransferRepository MoneyTransferRepository { get; }
        public GenericRepository<InnerMoneyAccount> InnerMoneyAccountRepository { get; }
        public GenericRepository<BankAccount> BankAccountRepository { get; }
        public GenericRepository<Photo> PhotoRepository { get; }
        public GenericRepository<PositiveVote> PositiveVoteRepository { get; }
        public GenericRepository<NegativeVote> NegativeVoteRepository { get; }

        public void Complete()
        {
            context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                {
                    context.Dispose();
                }
            disposed = true;
        }
    }
}
