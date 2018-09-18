using CrowdfundingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfundingSystem.Data
{
    public static class SampleData
    {
        public static void Initialize(CrowdfundingSystemContext context, UserManager<User> userManager)
        {
            context.Database.EnsureDeleted();//remove these 2 commands later and use the Migrate() method
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (!context.CrowdfundingSystemOwners.Any())//don't delete this segment even in production (owner must be in the database from the beginning)
            {
                context.CrowdfundingSystemOwners.Add(
                    new CrowdfundingSystemOwner
                    {
                    });
            }

            if (!context.Users.Any())
            {
                var firstUser = new User
                {
                    FirstName = "Alex",
                    LastName = "Ford",
                    Email = "alex_ford@gmail.com",
                    UserName = "alex_ford@gmail.com",
                    AccountIsDeleted = false
                };
                userManager.CreateAsync(firstUser, "userPassword1").Wait();

                var secondUser = new User
                {
                    FirstName = "Anastasia",
                    LastName = "Oleschuk",
                    Email = "nastya.oleschuk@gmail.com",
                    UserName = "nastya.oleschuk@gmail.com",
                    AccountIsDeleted = false

                };
                userManager.CreateAsync(secondUser, "userPassword2").Wait();

                var thirdUser = new User
                {
                    FirstName = "Andrew",
                    LastName = "Lucas",
                    Email = "lucas@gmail.com",
                    UserName = "lucas@gmail.com",
                    AccountIsDeleted = false
                };
                userManager.CreateAsync(thirdUser, "userPassword3").Wait();

                context.SaveChanges();
            };

            //if (!context.Currencies.Any())
            //{
            //    context.Add(
            //        new BankAccountCurrency
            //        {
            //            Currency = Currency.USD
            //        }
            //        );
            //    context.SaveChanges();
            //}

            //if (!context.Banks.Any())
            //{
            //    context.Banks.Add(
            //        new Bank
            //        {
            //            BankName = "PrivatBank"
            //        }
            //    );
            //    context.SaveChanges();
            //}
            if (!context.BankAccounts.Any())
            {
                context.BankAccounts.AddRange(
                    new BankAccount //don't delete this segment even in production (Crowdfunding System owner's bank account must be in the database from the beginning)
                    {
                        //BankId = context.Banks.FirstOrDefault().Id,
                        //BankAccountCurrencyId = context.Currencies.FirstOrDefault().Id,
                        AccountOwnerId = Convert.ToString(context.CrowdfundingSystemOwners.FirstOrDefault().Id),
                        AccountNumber = "11111"
                    },
                    new BankAccount
                    {
                        //BankId = context.Banks.FirstOrDefault().Id,
                        //BankAccountCurrencyId = context.Currencies.FirstOrDefault().Id,
                        AccountOwnerId = context.Users.Where(u => u.Email == "alex_ford@gmail.com").FirstOrDefault().Id,
                        AccountNumber = "123",
                        SumAvailable = 10000000000
                    },
                    new BankAccount
                    {
                        //BankId = context.Banks.FirstOrDefault().Id,
                        //BankAccountCurrencyId = context.Currencies.FirstOrDefault().Id,
                        AccountOwnerId = context.Users.Where(u => u.Email == "nastya.oleschuk@gmail.com").FirstOrDefault().Id,
                        AccountNumber = "234",
                        SumAvailable = 10000000000
                    },
                    new BankAccount
                    {
                        //BankId = context.Banks.FirstOrDefault().Id,
                        //BankAccountCurrencyId = context.Currencies.FirstOrDefault().Id,
                        AccountOwnerId = context.Users.Where(u => u.Email == "lucas@gmail.com").FirstOrDefault().Id,
                        AccountNumber = "345",
                        SumAvailable = 10000000000
                    }
                    );
                context.SaveChanges();
            }
         
            //if (!context.Photos.Any())
            //{
            //    context.Photos.AddRange(

            //        new Photo
            //        {
            //            Url = "SomeUrl1"
            //        },
            //        new Photo
            //        {
            //            Url = "SomeUrl2"
            //        },
            //        new Photo
            //        {
            //            Url = "SomeUrl3"
            //        }
            //        );
            //}
            if (!context.Ideas.Any())
            {
                context.Ideas.AddRange(
                    new Idea
                    {
                        IdeaName = "MusicOnlineFestival",
                        OwnerId = context.Users.Where(u => u.Email == "alex_ford@gmail.com").FirstOrDefault().Id,
                        Description = "I would really like to organize a Music Online Festival",
                        SumRequired = 100000,
                        BankAccountId = context.BankAccounts.Where(b => (b.AccountOwnerId == context.Users.Where(u => u.Email == "alex_ford@gmail.com").FirstOrDefault().Id)).FirstOrDefault().Id,
                        DateOfPublishing = new DateTime(2018, 05, 03),
                        DeadlineForMoneyCollecting = new DateTime(2018, 10, 15),
                        IsStillCollectingMoney = true,
                        CollectedTheMoney = false
                    },
                new Idea
                {
                    IdeaName = "ArtOnlineFestival",
                    OwnerId = context.Users.Where(u => u.Email == "nastya.oleschuk@gmail.com").FirstOrDefault().Id,
                    Description = "I would really like to organize an Art Online Festival",
                    SumRequired = 105000,
                    BankAccountId = context.BankAccounts.Where(b => (b.AccountOwnerId == context.Users.Where(u => u.Email == "nastya.oleschuk@gmail.com").FirstOrDefault().Id)).FirstOrDefault().Id,
                    DateOfPublishing = new DateTime(2018, 02, 10),
                    DeadlineForMoneyCollecting = new DateTime(2018, 03, 10),
                    IsStillCollectingMoney = false,
                    CollectedTheMoney = true,
                    DateOfGoalAchieving = new DateTime(2018, 03, 05)
                },
                new Idea
                {
                    IdeaName = "exchangeStuffPlatform",
                    OwnerId = context.Users.Where(u => u.Email == "lucas@gmail.com").FirstOrDefault().Id,
                    Description = "I would really like to create an Exchange Stuff Platform ",
                    SumRequired = 50000,
                    BankAccountId = context.BankAccounts.Where(b => (b.AccountOwnerId == context.Users.Where(u => u.Email == "lucas@gmail.com").FirstOrDefault().Id)).FirstOrDefault().Id,
                    DateOfPublishing = new DateTime(2018, 06, 15),
                    DeadlineForMoneyCollecting = new DateTime(2018, 10, 15),
                    IsStillCollectingMoney = true,
                    CollectedTheMoney = false
                }
                );
                context.SaveChanges();
            }

            if (!context.InnerMoneyAccounts.Any())
            {
                context.InnerMoneyAccounts.AddRange(
                    new InnerMoneyAccount
                    {
                        IdeaId = context.Ideas.Where(i => i.IdeaName == "MusicOnlineFestival").FirstOrDefault().Id,
                    },
                new InnerMoneyAccount
                {
                    IdeaId = context.Ideas.Where(i => i.IdeaName == "ArtOnlineFestival").FirstOrDefault().Id,
                },
                new InnerMoneyAccount
                {
                    IdeaId = context.Ideas.Where(i => i.IdeaName == "exchangeStuffPlatform").FirstOrDefault().Id,
                });
            }

            if (!context.PositiveVotes.Any())
            {
                context.PositiveVotes.AddRange(
                    new PositiveVote
                    {
                        VoterId = context.Users.Where(u => u.Email == "nastya.oleschuk@gmail.com").FirstOrDefault().Id,
                        IdeaId = context.Ideas.Where(i => i.IdeaName == "MusicOnlineFestival").FirstOrDefault().Id
                    },
                    new PositiveVote
                    {
                        VoterId = context.Users.Where(u => u.Email == "lucas@gmail.com").FirstOrDefault().Id,
                        IdeaId = context.Ideas.Where(i => i.IdeaName == "MusicOnlineFestival").FirstOrDefault().Id
                    },
                    new PositiveVote
                    {
                        VoterId = context.Users.Where(u => u.Email == "lucas@gmail.com").FirstOrDefault().Id,
                        IdeaId = context.Ideas.Where(i => i.IdeaName == "ArtOnlineFestival").FirstOrDefault().Id
                    }
                    );
                context.SaveChanges();
            }
            if (!context.MoneyTransfers.Any())
            {
                context.MoneyTransfers.AddRange(
                    new MoneyTransfer
                    {
                        IdeaId = context.Ideas.Where(i => i.IdeaName == "MusicOnlineFestival").FirstOrDefault().Id,
                        SenderAccountId = context.BankAccounts.Where(a => a.AccountNumber == "234").FirstOrDefault().Id,
                        TransferredSum = 70,
                        DateOfTransfer = new DateTime(2018, 4, 15)
                    },
                    new MoneyTransfer
                    {
                        IdeaId = context.Ideas.Where(i => i.IdeaName == "MusicOnlineFestival").FirstOrDefault().Id,
                        SenderAccountId = context.BankAccounts.Where(a => a.AccountNumber == "345").FirstOrDefault().Id,
                        TransferredSum = 50,
                        DateOfTransfer = new DateTime(2018, 4, 18)
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
