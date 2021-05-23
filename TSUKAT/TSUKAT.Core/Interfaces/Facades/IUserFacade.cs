using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Core.Interfaces.Facades
{
    public interface IUserFacade
    {
        Task<IEnumerable<UserViewModel>> GetUserViewModelsAsync();
        Task<UserViewModel> GetUserAsync(int userId);
        Task<UserViewModel> UpdateUserAsync(UserViewModel user, User authenticatedUser);
        Task<UserViewModel> AddUserAsync(UserViewModel user, User authenticatedUser);
        Task<UserViewModel> DeleteUserAsync(int id);
    }
}
