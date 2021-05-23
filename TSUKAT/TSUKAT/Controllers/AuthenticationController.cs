using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Helpers.Extentions;
using TSUKAT.Core.Interfaces.Services;
using TSUKAT.Core.Models.Autentication;

namespace TSUKAT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> GetJwsToken(AuthenticateRequest authenticateRequest)
        {
            try
            {
                User user = null;

                if (authenticateRequest.Username != null && authenticateRequest.Password != null)
                {
                    user = await _userService.GetUserAsync(authenticateRequest.Username, authenticateRequest.Password);

                    if (user == null)
                        return Unauthorized("Invalid username, password");
                }


                if (authenticateRequest.AccessToken != null && user == null)
                    user = await _userService.GetUserAsync(authenticateRequest.AccessToken);

                var identity = _authenticationService.GetIdentity(user);

                if (identity == null)
                    return Unauthorized("Invalid access token.");

                var encodedJwt = _authenticationService.GenerateJwtSecurityToken(identity);

                var response = new AuthenticateResponse
                {
                    Token = encodedJwt,
                    UserId = user.Id,
                    RedirectUrl = authenticateRequest.RedirectUrl,
                    UserName = user.UserName,
                    Roles = user.UserRoles.Select(x => x.Role.ToRoleViewModel()).ToList(),
                    Name = user.Name,
                    SurName = user.Surname,
                    Group = user.Group.ToGroupViewModel(),
                    Password = user.Password
                };


                return new JsonResult(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
