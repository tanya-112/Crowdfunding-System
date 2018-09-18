using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfundingSystem.Data.Interfaces
{
    public interface IIdeaRepository : IRepository<Idea>
    {
        Idea GetById(int id, string includeProperties = "");
        Task<Idea> GetByIdAsync(int id, string includeProperties = "");
    }
}
