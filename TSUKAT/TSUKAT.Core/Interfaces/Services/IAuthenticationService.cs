using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TSUKAT.Core.DbModels;

namespace TSUKAT.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        string GenerateJwtSecurityToken(ClaimsIdentity identity);
        ClaimsIdentity GetIdentity(User user);
    }
}
