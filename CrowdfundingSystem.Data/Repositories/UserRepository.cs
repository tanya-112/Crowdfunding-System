using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CrowdfundingSystemContext context) : base(context)
        {
        }
        public CrowdfundingSystemContext Context
        {
            get
            {
                return context as CrowdfundingSystemContext;
            }
        }
        public async Task<User> GetByIdAsync(string id, string includeProperties = "")
        {
            IQueryable<User> result = Context.Users;
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }
            return await result.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
    }
}
