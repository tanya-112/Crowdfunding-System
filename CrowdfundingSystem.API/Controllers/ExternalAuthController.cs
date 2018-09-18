using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CrowdfundingSystem.API.Options;
using CrowdfundingSystem.API.Services.Interfaces;
using CrowdfundingSystem.Data;
using CrowdfundingSystem.Data.Entities;
using CrowdfundingSystem.API.ResourceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using CrowdfundingSystem.API.Helpers;

namespace CrowdfundingSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ExternalAuthController : ControllerBase
    {
        private readonly CrowdfundingSystemContext context; //integrate the methods with the unit of work if possible, so the context isn't needed explicitly
        private readonly UserManager<User> userManager;
        private readonly FacebookAuthSettings fbAuthSettings;
        private readonly IJwtFactory jwtFactory;
        private readonly JwtIssuerOptions jwtOptions;
        private static readonly HttpClient client = new HttpClient();

        public ExternalAuthController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor, UserManager<User> userManager,
            CrowdfundingSystemContext context, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            fbAuthSettings = fbAuthSettingsAccessor.Value;
            this.userManager = userManager;
            this.context = context;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions.Value;
        }

        // POST api/externalauth/facebook
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthResourceModel model)
        {
            // 1.generate an app access token
            var appAccessTokenResponse = await client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={fbAuthSettings.AppId}&client_secret={fbAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

            // 2. validate the user access token
            var userAccessTokenValidationResponse = await client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid Facebook token.", ModelState));
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new User
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    ProfilePhoto = new Photo { Url = userInfo.Picture.Data.Url }
                };
                var result = await userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));
                if (!result.Succeeded)
                    return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
                await context.SaveChangesAsync();
            }
            // generate the jwt for the local user
            var localUser = await userManager.FindByEmailAsync(userInfo.Email);
            if (localUser == null)
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
            var jwt = await Tokens.GenerateJwt(jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id), jwtFactory, localUser.UserName, jwtOptions,
                new JsonSerializerSettings { Formatting = Formatting.Indented });

            return Ok(jwt);
        }
    }
}