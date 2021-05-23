using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSUKAT.Core.DbModels;

namespace TSUKAT.Core.Interfaces.Services
{
    public interface IGroupService
    {
        public Task<IEnumerable<Group>> GetGroupsAsync();
        public Task<Group> GetGroupAsync(int id);
        public Task<Group> AddGroupAsync(Group group);
        public Task<Group> UpdateGroupAsync(Group group);
        public Task<Group> DeleteGroupAsync(int id);
        public Task<IEnumerable<Role>> GetRolesAsync();
    }
}
