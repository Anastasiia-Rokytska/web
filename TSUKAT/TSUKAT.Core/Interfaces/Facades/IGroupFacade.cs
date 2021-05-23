using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Core.Interfaces.Facades
{
    public interface IGroupFacade
    {
        public Task<IEnumerable<GroupViewModel>> GetGroupsAsync();
        public Task<GroupViewModel> GetGroupAsync(int id);
        public Task<GroupViewModel> AddGroupAsync(GroupViewModel group);
        public Task<GroupViewModel> UpdateGroupAsync(GroupViewModel group);
        public Task<GroupViewModel> DeleteGroupAsync(int id);

        public Task<RoleGroupViewModel> GetRoleGroupViewModel();
    }
}
