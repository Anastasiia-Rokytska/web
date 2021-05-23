using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Core.Helpers.Extentions
{
    public static class Extention
    {
        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Password = user.Password,
                AccessToken = user.AccessToken,
                Group = user.Group?.ToGroupViewModel(),
                Roles = user.UserRoles.Select(role => role.Role.ToRoleViewModel()).ToList()
            };
        }
        public static RoleViewModel ToRoleViewModel(this Role role)
        {
            return new RoleViewModel
            {
                RoleId = role.Id,
                Name = role.Name
            };
        }
        public static GroupViewModel ToGroupViewModel(this Group group)
        {
            return new GroupViewModel
            {
                GroupId = group.Id,
                GroupName = group.GroupName,
                Users = group.Users.Select(user => new UserViewModel
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    Password = user.Password,
                    AccessToken = user.AccessToken,
                    Roles = user.UserRoles.Select(role => role.Role.ToRoleViewModel()).ToList()
                }).ToList()
            };
        }

        public static User ToUserModel(this UserViewModel user)
        {
            return new User
            {
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Password = user.Password,
                AccessToken = user.AccessToken,
                Group = user.Group?.ToGroupModel(),
                UserRoles = user.Roles.Select(role => 
                {
                    return new UserRole
                    {
                        RoleId = role.RoleId
                    };
                }).ToList()
            };
        }
        public static Group ToGroupModel(this GroupViewModel group)
        {
            return new Group
            {
                Id = group.GroupId,
                GroupName = group.GroupName
            };
        }

        public static RoleViewModel ToUserRoleViewModel(this UserRole role)
        {
            return new RoleViewModel
            {
                RoleId = role.RoleId,
                Name = role.Role.Name
            };
        }

        public static UserRole ToUserRoleModel(this RoleViewModel role)
        {
            return new UserRole()
            {
                RoleId = role.RoleId
            };
        }
    }
}
