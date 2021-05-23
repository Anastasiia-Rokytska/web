using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Interfaces.Services;

namespace TSUKAT.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IDataContext _dataContext;

        public GroupService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Group> AddGroupAsync(Group group)
        {
            var newGroup = await _dataContext.Groups.AddAsync(group);
            await _dataContext.SaveChangesAsync();
            return newGroup.Entity;
        }

        public async Task<Group> DeleteGroupAsync(int id)
        {
            var group = await GetGroupAsync(id);
            var deletedGroup = _dataContext.Groups.Remove(group);
            await _dataContext.SaveChangesAsync();
            return deletedGroup.Entity;
        }

        public async Task<Group> GetGroupAsync(int id)
        {
            var group = await _dataContext.Groups.Include(u => u.Users).FirstOrDefaultAsync(x => x.Id == id);
            return group;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _dataContext.Groups.Include(u => u.Users).ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _dataContext.Roles.ToListAsync();
        }

        public async Task<Group> UpdateGroupAsync(Group group)
        {
            var updatedGroup = _dataContext.Groups.Update(group);
            await _dataContext.SaveChangesAsync();
            return updatedGroup.Entity;
        }
    }
}
