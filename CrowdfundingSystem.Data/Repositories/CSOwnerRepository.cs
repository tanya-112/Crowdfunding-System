using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CrowdfundingSystem.Data.Repositories
{
    public class CSOwnerRepository : GenericRepository<CrowdfundingSystemOwner>, ICSOwnerRepository
    {
        public CSOwnerRepository(CrowdfundingSystemContext context) : base(context)
        {
        }
       
    }
}
