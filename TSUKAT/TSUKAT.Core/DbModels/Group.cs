using System;
using System.Collections.Generic;
using System.Text;

namespace TSUKAT.Core.DbModels
{
    public class Group : BaseEntity
    {
        public string GroupName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
