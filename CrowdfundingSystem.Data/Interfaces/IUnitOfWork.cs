using CrowdfundingSystem.Data.Repositories;
using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfundingSystem.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IIdeaRepository IdeaRepository { get; }
        IUserRepository UserRepository { get; }
        GenericRepository<InnerMoneyAccount> InnerMoneyAccountRepository { get; }
        GenericRepository<BankAccount> BankAccountRepository { get; }
        ICSOwnerRepository CSOwnerRepository { get; }
        IMoneyTransferRepository MoneyTransferRepository { get; }
        GenericRepository<PositiveVote> PositiveVoteRepository { get; }
        GenericRepository<NegativeVote> NegativeVoteRepository { get; }
        GenericRepository<Photo> PhotoRepository { get; }

        void Complete();
        Task CompleteAsync();

    }
}
