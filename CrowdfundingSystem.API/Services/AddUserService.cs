using CrowdfundingSystem.API.Services.Interfaces;
using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.ResourceModels.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services
{
    public class AddUserService : IAddUserService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<User> userManager;
        public AddUserService(IHostingEnvironment hostingEnvironment, UserManager<User> userManager)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }
        public async Task<User> AddUser(IUnitOfWork unitOfWork, UserPostResourceModel userPostResourceModel)
        {
            var profilePhoto = userPostResourceModel.ProfilePhoto;

            Photo photo = new Photo();
            if (profilePhoto != null)
            {
                var pathInsideTheWwwRoot = "Images/" + Guid.NewGuid() + Path.GetFileName(profilePhoto.FileName);
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await profilePhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));
                photo = new Photo()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };

            }
            User user;
            if(photo.Url!=null)
            user = new User()
            {
                FirstName = userPostResourceModel.FirstName,
                LastName = userPostResourceModel.LastName,
                Email = userPostResourceModel.Email,
                UserName = userPostResourceModel.UserName,
                DateOfBirth = userPostResourceModel.DateOfBirth,
                Country = userPostResourceModel.Country,
                City = userPostResourceModel.City,
                ProfilePhoto = photo,
                Bio = userPostResourceModel.Bio,
                //Password = userPostResourceModel.Password,
                Interests = userPostResourceModel.Interests,
                //BankAccounts = userPostResourceModel.BankAccounts
            };
            else
                user = new User()
                {
                    FirstName = userPostResourceModel.FirstName,
                    LastName = userPostResourceModel.LastName,
                    Email = userPostResourceModel.Email,
                    UserName = userPostResourceModel.UserName,
                    DateOfBirth = userPostResourceModel.DateOfBirth,
                    Country = userPostResourceModel.Country,
                    City = userPostResourceModel.City,
                    Bio = userPostResourceModel.Bio,
                    //Password = userPostResourceModel.Password,
                    Interests = userPostResourceModel.Interests,
                    //BankAccounts = userPostResourceModel.BankAccounts
                };
            var result= await userManager.CreateAsync(user, userPostResourceModel.Password);

            if (!result.Succeeded)
                return await Task.FromResult<User>(null);

            //if (photo.Url != "")
                //await unitOfWork.PhotoRepository.InsertAsync(photo); // when calling unitOfWork.Complete() in the controller the photo gets inserted, so this line isn't needed
            return user;
        }
    }
}
