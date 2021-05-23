using System;
using System.Collections.Generic;
using System.Text;

namespace TSUKAT.Core.Models.General
{
    public class RoleGroupViewModel
    {
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public IEnumerable<GroupViewModel> Groups { get; set; }
    }
}
