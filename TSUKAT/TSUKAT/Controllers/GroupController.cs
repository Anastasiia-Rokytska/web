using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSUKAT.Core.Interfaces.Facades;
using TSUKAT.Core.Interfaces.Services;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Administrator")]
    [ApiController]
    public class GroupController : AuthenticatedUserController
    {
        private readonly IGroupFacade _groupFacade;
        public GroupController(IUserService userService, IGroupFacade groupFacade) : base(userService)
        {
             _groupFacade = groupFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                await SetAuthenticatedUser();

                var groups = await _groupFacade.GetGroupsAsync();

                return new JsonResult(groups);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("getRoleGroupModel")]
        public async Task<IActionResult> GetRoleGroupModel()
        {
            try
            {
                await SetAuthenticatedUser();

                var groups = await _groupFacade.GetRoleGroupViewModel();

                return new JsonResult(groups);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                await SetAuthenticatedUser();

                var groups = await _groupFacade.GetGroupAsync(id);

                return new JsonResult(groups);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupViewModel group)
        {
            try
            {
                await SetAuthenticatedUser();

                var groups = await _groupFacade.UpdateGroupAsync(group);

                return new JsonResult(groups);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] GroupViewModel group)
        {
            try
            {
                await SetAuthenticatedUser();

                var groups = await _groupFacade.AddGroupAsync(group);

                return new JsonResult(groups);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateGroup(int id)
        {
            try
            {
                await SetAuthenticatedUser();

                var groups = await _groupFacade.DeleteGroupAsync(id);

                return new JsonResult(groups);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
