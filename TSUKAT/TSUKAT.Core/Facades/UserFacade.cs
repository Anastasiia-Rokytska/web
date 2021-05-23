using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Helpers.Extentions;
using TSUKAT.Core.Interfaces.Facades;
using TSUKAT.Core.Interfaces.Services;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Core.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;

        public UserFacade(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserViewModel> DeleteUserAsync(int id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            return deletedUser.ToUserViewModel();

        }

        public async Task<UserViewModel> GetUserAsync(int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return user.ToUserViewModel();
        }

        public async Task<IEnumerable<UserViewModel>> GetUserViewModelsAsync()
        {
            var users = await _userService.GetUsersAsync();
            var userViewModels = users.Select(user => user.ToUserViewModel()).ToList();
            return userViewModels;
        }

        public async Task<UserViewModel> UpdateUserAsync(UserViewModel updatedUser, User authenticatedUser)
        {
            if (updatedUser is null)
            {
                throw new ArgumentNullException(nameof(updatedUser));
            }

            await ValidateUser(updatedUser);

            var userToUpdate = await _userService.GetUserAsync(updatedUser.UserId);

            userToUpdate.AccessToken = updatedUser.AccessToken;
            userToUpdate.Password = updatedUser.Password;
            userToUpdate.UserName = updatedUser.UserName;
            userToUpdate.UserRoles.Clear();
            userToUpdate.Name = updatedUser.Name;
            userToUpdate.Surname = updatedUser.Surname;
            userToUpdate.UserRoles = updatedUser.Roles.Select(role => role.ToUserRoleModel()).ToList();

            var user = await _userService.UpdateUserAsync(userToUpdate);
            return updatedUser;
        }

        public async Task<UserViewModel> AddUserAsync(UserViewModel newUser, User authenticatedUser)
        {
            if (newUser is null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }

            await ValidateUser(newUser);
            var roles = newUser.Roles.Select(role => role.ToUserRoleModel()).ToList();

            var user = new User
            {
                AccessToken = newUser.AccessToken,
                Password = newUser.Password,
                UserName = newUser.UserName,
                UserRoles = roles,
                Name = newUser.Name,
                Surname = newUser.Surname
            };

            await _userService.AddUserAsync(user);

            return newUser;
        }

        private async Task ValidateUser(UserViewModel user)
        {
            var users = await _userService.GetUsersAsync();
            if (users.Any(x => x.UserName.ToLower() == user.UserName.ToLower() && x.Id != user.UserId))
            {
                throw new Exception("Username already user!");
            }
            if (users.Any(x => x.AccessToken.ToLower() == user.AccessToken.ToLower() && x.Id != user.UserId))
            {
                throw new Exception("AccessToken already used!");
            }
        }
    }
}
