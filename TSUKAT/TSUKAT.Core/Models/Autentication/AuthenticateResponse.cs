using System;
using System.Collections.Generic;
using System.Text;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Models.General;

namespace TSUKAT.Core.Models.Autentication
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
        public GroupViewModel Group { get; set; }
        public string Password { get; set; }
    }
}
