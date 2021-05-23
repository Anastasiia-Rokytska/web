using System;
using System.Collections.Generic;
using System.Text;

namespace TSUKAT.Core.Models.General
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public virtual ICollection<RoleViewModel> Roles { get; set; }
        public virtual GroupViewModel Group { get; set; }
    }
}
