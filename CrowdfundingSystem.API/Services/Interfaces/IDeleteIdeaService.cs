using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services.Interfaces
{
    public interface IDeleteIdeaService
    {
        bool DeleteIdea(IUnitOfWork unitOfWork, int id);
    }
}
