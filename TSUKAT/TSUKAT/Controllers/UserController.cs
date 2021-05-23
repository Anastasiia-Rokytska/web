using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSUKAT.Core.Enums;
using TSUKAT.Core.Interfaces.Facades;
using TSUKAT.Core.Interfaces.Services;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : AuthenticatedUserController
    {
        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade, IUserService userService) : base(userService)
        {
            _userFacade = userFacade;
        }

        [HttpGet]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                await SetAuthenticatedUser();

                if (HasRole(UserRoles.Administrator))
                {

                    var users = await _userFacade.GetUserViewModelsAsync();
                    return new JsonResult(users);
                }
                else
                {
                    var user = await _userFacade.GetUserAsync(AuthenticatedUser.Id);
                    return new JsonResult(user);
                }

            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                await SetAuthenticatedUser();

                if(id != AuthenticatedUser.Id && !HasRole(UserRoles.Administrator))
                {
                    throw new UnauthorizedAccessException("Access denied");
                }

                var user = await _userFacade.GetUserAsync(id);

                return new JsonResult(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("addUser")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddUser([FromBody] UserViewModel user)
        {
            try
            {
                await SetAuthenticatedUser();

                var newUser = await _userFacade.AddUserAsync(user, AuthenticatedUser);

                return new JsonResult(newUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("updateUser")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> UpdateUser([FromBody] UserViewModel user)
        {
            try
            {
                await SetAuthenticatedUser();

                if (user.UserName != AuthenticatedUser.UserName && !HasRole(UserRoles.Administrator))
                {
                    throw new UnauthorizedAccessException("Access denied");
                }

                var newUser = await _userFacade.UpdateUserAsync(user, AuthenticatedUser);

                return new JsonResult(newUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await SetAuthenticatedUser();

                var newUser = await _userFacade.DeleteUserAsync(id);

                return new JsonResult(newUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}
