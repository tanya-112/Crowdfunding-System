using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.ResourceModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services.Interfaces
{
    public interface IEditUserService
    {
        Task<User> EditUserAsync(IUnitOfWork unitOfWork, string userId, UserPutResourceModel userPostResourceModel);
    }
}
