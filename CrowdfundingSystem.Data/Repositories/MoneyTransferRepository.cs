using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace CrowdfundingSystem.Data.Repositories
{
    public class MoneyTransferRepository: GenericRepository<MoneyTransfer>, IMoneyTransferRepository
    {
        public MoneyTransferRepository(CrowdfundingSystemContext context) : base(context)
        {
        }
        public CrowdfundingSystemContext Context
        {
            get
            {
                return context as CrowdfundingSystemContext;
            }
        }

        public MoneyTransfer GetById(int id, string includeProperties = "")
        {
            IQueryable<MoneyTransfer> result = Context.MoneyTransfers;
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }
            return result.Where(i => i.Id == id).FirstOrDefault();
        }

        public async Task<MoneyTransfer> GetByIdAsync(int id, string includeProperties = "")
        {
            IQueryable<MoneyTransfer> result = Context.MoneyTransfers;
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }
            return await result.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
    }
}
