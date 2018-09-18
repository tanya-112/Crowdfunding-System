using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.Data.Repositories
{
    public class IdeaRepository : GenericRepository<Idea>, IIdeaRepository
    {
        public IdeaRepository(CrowdfundingSystemContext context) : base(context)
        {
        }

        public Idea GetById(int id, string includeProperties = "")
        {
            IQueryable<Idea> result = Context.Ideas;
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }
            return result.Where(i => i.Id == id).FirstOrDefault();
        }


        public async Task<Idea> GetByIdAsync(int id, string includeProperties = "")
        {
            IQueryable<Idea> result = Context.Ideas;
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }
            return await result.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

       
        public CrowdfundingSystemContext Context
        {
            get
            {
                return context as CrowdfundingSystemContext;
            }
        }

    }
}
