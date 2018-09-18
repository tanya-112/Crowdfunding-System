using CrowdfundingSystem.API.Services.Interfaces;
using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.ResourceModels.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Services
{
    public class EditUserService : IEditUserService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public EditUserService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<User> EditUserAsync(IUnitOfWork unitOfWork, string userId, UserPutResourceModel userPutResourceModel)
        {
            Photo photo = new Photo();
            var profilePhoto = userPutResourceModel.ProfilePhoto;
            if (profilePhoto != null)
            {
                var pathInsideTheWwwRoot = "Images/" + Guid.NewGuid() + Path.GetFileName(profilePhoto.FileName);
                var fileName = Path.Combine(hostingEnvironment.WebRootPath, pathInsideTheWwwRoot);
                await profilePhoto.CopyToAsync(new FileStream(fileName, FileMode.Create));
                photo = new Photo()
                {
                    //Url = fileName
                    Url = "/" + pathInsideTheWwwRoot
                };
            }
            var userToUpdate = await unitOfWork.UserRepository.GetByIdAsync(userId, includeProperties: "ProfilePhoto");
            if (userToUpdate == null)
                return await Task.FromResult<User>(null);
            if (photo.Url != "" && userToUpdate.ProfilePhoto != null)
                await unitOfWork.PhotoRepository.DeleteAsync(userToUpdate.ProfilePhoto.Id); //deleting user's previous ProfilePhoto


            userToUpdate.FirstName = userPutResourceModel.FirstName;
            userToUpdate.LastName = userPutResourceModel.LastName;
            userToUpdate.Email = userPutResourceModel.Email;
            userToUpdate.DateOfBirth = userPutResourceModel.DateOfBirth;
            userToUpdate.Country = userPutResourceModel.Country;
            userToUpdate.City = userPutResourceModel.City;
            userToUpdate.ProfilePhoto = photo;
            userToUpdate.Bio = userPutResourceModel.Bio;
            //Password = userPutResourceModel.Password,
            userToUpdate.Interests = userPutResourceModel.Interests;
            userToUpdate.BankAccounts = userPutResourceModel.BankAccounts;

            unitOfWork.UserRepository.Update(userToUpdate);
            if (photo.Url != "")
                await unitOfWork.PhotoRepository.InsertAsync(photo);
            return userToUpdate;
        }
    }
}
