using System;
using System.Collections.Generic;
using System.Text;

namespace TSUKAT.Core.DbModels
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
