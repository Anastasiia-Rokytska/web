using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TSUKAT.Core.DbModels;

namespace TSUKAT.Core
{
    public interface IDataContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Group> Groups { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
