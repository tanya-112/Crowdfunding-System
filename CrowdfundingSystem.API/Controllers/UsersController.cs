using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrowdfundingSystem.Data;
using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.Data.Interfaces;
using CrowdfundingSystem.Data.ResourceModels.User;
using CrowdfundingSystem.API.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace CrowdfundingSystem.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAddUserService addUserService;
        private readonly IEditUserService editUserService;

        private readonly ClaimsPrincipal caller;


        public UsersController(IUnitOfWork unitOfWork, IAddUserService addUserService, IEditUserService editUserService, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.addUserService = addUserService;
            this.editUserService = editUserService;
            caller = httpContextAccessor.HttpContext.User;

        }

        //// GET: api/Users
        //[HttpGet]
        //public async Task<IEnumerable<UsersGetAllResourceModel>> Get()
        //{
        //    var users = await unitOfWork.UserRepository.GetAsync(filter: u => u.AccountIsDeleted == false, includeProperties: "Interests");
        //    var usersToReturn = users.Select(u => new UsersGetAllResourceModel()
        //    {
        //        Id = u.Id,
        //        FirstName = u.FirstName,
        //        LastName = u.LastName,
        //        Country = u.Country,
        //        City = u.City,
        //        ProfilePhotoId = u.ProfilePhotoId,
        //        ProfilePhoto = u.ProfilePhoto,
        //        Bio = u.Bio,
        //        Interests = u.Interests
        //    });
        //    return usersToReturn;
        //}

        // GET: api/Users/5
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = caller.Claims.Single(c => c.Type == "id");

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }
            var userToReturn = new UserGetResourceModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country,
                City = user.City,
                ProfilePhotoId = user.ProfilePhotoId,
                ProfilePhoto = user.ProfilePhoto,
                Bio = user.Bio,
                AccountIsDeleted = user.AccountIsDeleted,
                Interests = user.Interests
            };
            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UserPutResourceModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedUser = await editUserService.EditUserAsync(unitOfWork,id, user);
            await unitOfWork.CompleteAsync();
            if (updatedUser == null)
                return BadRequest();
            return Ok(updatedUser);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Post(UserPostResourceModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedUser = await addUserService.AddUser(unitOfWork, user);

            await unitOfWork.CompleteAsync();
            if (addedUser != null)
                //return CreatedAtAction("Get", new { id = addedUser.Id }, addedUser);
                return Ok(addedUser);
            else
                return BadRequest();
        }

        // Deletion of User Account is not supported in the current version of the Crowdfunding System. May be supported later

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    bool deletionSuccesed = await unitOfWork.UserRepository.DeleteAsync(id);

        //    if (!deletionSuccesed)
        //    {
        //        return NotFound();
        //    }

        //    await unitOfWork.CompleteAsync();
        //    return Ok();
        //}

    }
}