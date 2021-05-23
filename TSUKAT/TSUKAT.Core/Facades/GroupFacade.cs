using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSUKAT.Core.Helpers.Extentions;
using TSUKAT.Core.Interfaces.Facades;
using TSUKAT.Core.Interfaces.Services;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Core.Facades
{
    public class GroupFacade : IGroupFacade
    {
        private readonly IGroupService _groupService;

        public GroupFacade(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<GroupViewModel> AddGroupAsync(GroupViewModel group)
        {
            var newGroup = await _groupService.AddGroupAsync(group.ToGroupModel());
            return newGroup.ToGroupViewModel();
        }

        public async Task<GroupViewModel> DeleteGroupAsync(int id)
        {
            var deletedGroup = await _groupService.DeleteGroupAsync(id);
            return deletedGroup.ToGroupViewModel();
        }

        public async Task<GroupViewModel> GetGroupAsync(int id)
        {
            var group = await _groupService.GetGroupAsync(id);
            return group.ToGroupViewModel();
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroupsAsync()
        {
            var groups = await _groupService.GetGroupsAsync();
            return groups.Select(x => x.ToGroupViewModel()).ToList();
        }

        public async Task<RoleGroupViewModel> GetRoleGroupViewModel()
        {
            var groups = await GetGroupsAsync();
            var roles = await _groupService.GetRolesAsync();
            var model = new RoleGroupViewModel
            {
                Roles = roles.Select(role => role.ToRoleViewModel()).ToList(),
                Groups = groups
            };
            return model;
        }

        public async Task<GroupViewModel> UpdateGroupAsync(GroupViewModel group)
        {
            var updatedGroup = await _groupService.UpdateGroupAsync(group.ToGroupModel());
            return updatedGroup.ToGroupViewModel();
        }
    }
}
