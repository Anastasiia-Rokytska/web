using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Enums;
using TSUKAT.Core.Interfaces.Services;

namespace TSUKAT.Api.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class AuthenticatedUserController : ControllerBase
    {
        protected AuthenticatedUserController(IUserService userService)
        {
            UserService = userService;
        }

        private IUserService UserService { get; }
        public string UserName { get; set; }
        public User AuthenticatedUser { get; set; }


        [NonAction]
        protected async virtual Task SetAuthenticatedUser()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity)) return;
            var name = identity.Claims.FirstOrDefault(x => x.Type.Contains("name"));

            UserName = name?.Value;

            AuthenticatedUser = await UserService.GetAuthorizedUserAsync(UserName);
        }

        [NonAction]
        protected virtual int? GetCurrentUserId()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity)) return null;
            var user = identity.Claims.FirstOrDefault(x => x.Type.Contains("UserId"));

            return user != null && int.TryParse(user.Value, out var userId) ? (int?)userId : null;
        }


        [NonAction]
        protected virtual bool HasRole(UserRoles userRole)
        {
            return AuthenticatedUser.UserRoles.Any(x => x.Role.Name == userRole.ToString());
        }
    }
}
