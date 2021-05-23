using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSUKAT.Core.DbModels;

namespace TSUKAT.Core.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int id);
        public Task<User> GetUserAsync(string accessToken);
        public Task<User> GetUserAsync(string username, string password);
        public Task<User> GetAuthorizedUserAsync(string username);
        public Task<User> AddUserAsync(User newUser);
        public Task<User> UpdateUserAsync(User user);
        public Task<User> DeleteUserAsync(int id);
    }
}
