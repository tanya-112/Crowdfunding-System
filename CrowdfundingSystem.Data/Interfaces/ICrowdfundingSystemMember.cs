using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrowdfundingSystem.Data.Interfaces
{
    public interface ICrowdfundingSystemMember
    {
        ICollection<BankAccount> BankAccounts { get; set; }
    }
    //public abstract class CrowdfundingSystemMember 
    //{
    //    public int Id { get; set; }

    //    public ICollection<BankAccount> BankAccounts { get; set; }
    //}
}
