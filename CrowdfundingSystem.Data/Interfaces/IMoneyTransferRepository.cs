using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfundingSystem.Data.Interfaces
{
    public interface IMoneyTransferRepository : IRepository<MoneyTransfer>
    {
        MoneyTransfer GetById(int id, string includeProperties = "");
        Task<MoneyTransfer> GetByIdAsync(int id, string includeProperties = "");
    }
}
