using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DiabloCms.Entities.Models;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Identity;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DiabloCms.UseCases.Services.Identity
{
    [Inject]
    public partial class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly ApplicationSettingJwt _appJwt;
        private readonly UserManager<CmsUser> _userManager;

        public async Task<string> GenerateJwtAsync(CmsUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Surname, user.LastName)
            };

            var isAdministrator = await _userManager.IsInRoleAsync(user, CmsUserRoles.AdminRole).ConfigureAwait(false);

            if (isAdministrator) claims.Add(new Claim(ClaimTypes.Role, CmsUserRoles.AdminRole));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(_appJwt.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}