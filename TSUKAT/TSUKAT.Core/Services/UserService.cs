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
    public class UserService : IUserService
    {
        private readonly IDataContext _dataContext;

        public UserService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(string accessToken)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.AccessToken == accessToken);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _dataContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            return await _dataContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }

        public async Task<User> GetAuthorizedUserAsync(string username)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                throw new NullReferenceException($"There is no user with username {username}");

            return user;
        }

        public async Task<User> AddUserAsync(User newUser)
        {
            var user = await _dataContext.Users.AddAsync(newUser);
            await _dataContext.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var t = _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();
            return await GetUserAsync(t.Entity.Id);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await GetUserAsync(id);
            var deleteUser = _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
            return deleteUser.Entity;
        }
    }
}
