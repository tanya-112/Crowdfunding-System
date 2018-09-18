﻿// <auto-generated />
using CrowdfundingSystem.Data;
using CrowdfundingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace CrowdfundingSystem.Data.Migrations
{
    [DbContext(typeof(CrowdfundingSystemContext))]
    [Migration("20180609175950_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BankName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber")
                        .IsRequired();

                    b.Property<string>("AccountOwnerId");

                    b.Property<int?>("AccountOwnerId1");

                    b.Property<int>("BankAccountCurrencyId");

                    b.Property<int>("BankId");

                    b.Property<decimal>("SumAvailable");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AccountOwnerId1");

                    b.HasIndex("BankAccountCurrencyId");

                    b.HasIndex("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.BankAccountCurrency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Currency");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.CrowdfundingSystemMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CrowdfundingSystemMember");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CrowdfundingSystemMember");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdeaId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Idea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BankAccountId");

                    b.Property<bool>("CollectedTheMoney");

                    b.Property<DateTime?>("DateOfGoalAchieving");

                    b.Property<DateTime>("DateOfPublishing");

                    b.Property<DateTime>("DeadlineForMoneyCollecting");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("IdeaName")
                        .IsRequired();

                    b.Property<bool>("IsStillCollectingMoney");

                    b.Property<int?>("MainPhotoId");

                    b.Property<int?>("MainVideoId");

                    b.Property<string>("OwnerId");

                    b.Property<decimal>("SumRequired");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("MainPhotoId");

                    b.HasIndex("MainVideoId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.InnerMoneyAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdeaId");

                    b.Property<decimal>("SumAvailable");

                    b.Property<bool>("SumTransferredToRealBankAccount");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId")
                        .IsUnique()
                        .HasFilter("[IdeaId] IS NOT NULL");

                    b.ToTable("InnerMoneyAccounts");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.MoneyTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CrowdfundingSystemOwnerId");

                    b.Property<DateTime>("DateOfTransfer");

                    b.Property<int>("IdeaId");

                    b.Property<int>("SenderAccountId");

                    b.Property<decimal>("TransferredSum");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CrowdfundingSystemOwnerId");

                    b.HasIndex("IdeaId");

                    b.HasIndex("SenderAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("MoneyTransfers");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.NegativeVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdeaId");

                    b.Property<string>("VoterId");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.HasIndex("VoterId");

                    b.ToTable("NegativeVotes");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdeaId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.PositiveVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdeaId");

                    b.Property<string>("VoterId");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.HasIndex("VoterId");

                    b.ToTable("PositiveVotes");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("AccountIsDeleted");

                    b.Property<string>("Bio");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Password");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int?>("ProfilePhotoId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProfilePhotoId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.UserInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Interest");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserInterest");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdeaId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.CrowdfundingSystemOwner", b =>
                {
                    b.HasBaseType("CrowdfundingSystem.Data.Entities.CrowdfundingSystemMember");


                    b.ToTable("CrowdfundingSystemOwner");

                    b.HasDiscriminator().HasValue("CrowdfundingSystemOwner");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.BankAccount", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.CrowdfundingSystemMember", "AccountOwner")
                        .WithMany("BankAccounts")
                        .HasForeignKey("AccountOwnerId1")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CrowdfundingSystem.Data.Entities.BankAccountCurrency", "BankAccountCurrency")
                        .WithMany()
                        .HasForeignKey("BankAccountCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CrowdfundingSystem.Data.Entities.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CrowdfundingSystem.Data.Entities.User")
                        .WithMany("BankAccounts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Document", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.Idea")
                        .WithMany("DocumentsAboutTheIdea")
                        .HasForeignKey("IdeaId");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Idea", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.BankAccount", "BankAccountForTransfers")
                        .WithMany()
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CrowdfundingSystem.Data.Entities.Photo", "MainPhoto")
                        .WithMany()
                        .HasForeignKey("MainPhotoId");

                    b.HasOne("CrowdfundingSystem.Data.Entities.Video", "MainVideo")
                        .WithMany()
                        .HasForeignKey("MainVideoId");

                    b.HasOne("CrowdfundingSystem.Data.Entities.User", "Owner")
                        .WithMany("UserIdeas")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.InnerMoneyAccount", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.Idea", "ForIdea")
                        .WithOne("InnerAccountForTransfers")
                        .HasForeignKey("CrowdfundingSystem.Data.Entities.InnerMoneyAccount", "IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.MoneyTransfer", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.CrowdfundingSystemOwner")
                        .WithMany("MoneyTransfersFromOwner")
                        .HasForeignKey("CrowdfundingSystemOwnerId");

                    b.HasOne("CrowdfundingSystem.Data.Entities.Idea", "Idea")
                        .WithMany("MoneyTransfers")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrowdfundingSystem.Data.Entities.BankAccount", "SenderAccount")
                        .WithMany()
                        .HasForeignKey("SenderAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CrowdfundingSystem.Data.Entities.User")
                        .WithMany("MoneyTransfersFromUser")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.NegativeVote", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.Idea", "Idea")
                        .WithMany("NegativeVotes")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrowdfundingSystem.Data.Entities.User", "Voter")
                        .WithMany("NegativeVotesFromUser")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Photo", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.Idea")
                        .WithMany("Photos")
                        .HasForeignKey("IdeaId");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.PositiveVote", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.Idea", "Idea")
                        .WithMany("PositiveVotes")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrowdfundingSystem.Data.Entities.User", "Voter")
                        .WithMany("PositiveVotesFromUser")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.User", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.Photo", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoId");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.UserInterest", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.User")
                        .WithMany("Interests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CrowdfundingSystem.Data.Entities.Video", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.Idea")
                        .WithMany("Videos")
                        .HasForeignKey("IdeaId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrowdfundingSystem.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CrowdfundingSystem.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
