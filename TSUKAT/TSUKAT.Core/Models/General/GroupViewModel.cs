using System;
using System.Collections.Generic;
using System.Text;

namespace TSUKAT.Core.Models.General
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<UserViewModel> Users { get; set; }
    }
}
