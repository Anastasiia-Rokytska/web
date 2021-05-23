using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TSUKAT.Core.DbModels;
using TSUKAT.Core.Interfaces.Services;

namespace TSUKAT.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthOptions _authOptions;

        public AuthenticationService(IOptions<AuthOptions> authConfig)
        {
            _authOptions = authConfig.Value;
        }


        public ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
                return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim("UserId", user.Id.ToString())
            };

            claims.AddRange(user.UserRoles.Select(x => new Claim(ClaimsIdentity.DefaultRoleClaimType, x.Role.Name))
                .ToList());

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }


        public string GenerateJwtSecurityToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            // generate token 
            var jwt = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
