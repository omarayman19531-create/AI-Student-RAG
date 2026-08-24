using Application.Interfaces.Auth;
using Domain.Entity;
using Domain.Entity.Auth;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Command.Auth
{
    public class TokenService(AppDbContext context,IConfiguration configuration,UserManager<Appuser>userManager) : ITokenService
    {
        public async Task<bool> AddRefreshToken(string userid, string refreshToken)
        {
            context.RefreshTokens.Add(new RefreshToken
            {
                UserId = userid,
                Token = refreshToken,
                Expiration = DateTime.UtcNow.AddDays(7)
            });
           var save= await context.SaveChangesAsync();
            return save > 0;
        }

        public  string GenerateRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(20);
            var token = new JwtSecurityToken
            (issuer: configuration["jwt:Issuer"]!,
                audience: configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: cred

                );
            return  new JwtSecurityTokenHandler().WriteToken(token);
            

        }

        public async Task<string?> GetUserByRefreshToken(string refreshToken)
        {
       var result= await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
          
            return result?.UserId;

        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
          var user= await  userManager.FindByEmailAsync(email);

            if (user==null)
            {
                return new List<Claim>();
            }
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.NameIdentifier,user.Id),
               new Claim(ClaimTypes.Email,user.Email),
              
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public async Task<bool> UpdateRefreshToken(string oldrefreshtoken, string newrefreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(e => e.Token == oldrefreshtoken);
            if (user == null || user.Expiration < DateTime.UtcNow) return false;

            user.Token = newrefreshToken;
            user.Expiration = DateTime.UtcNow.AddDays(7);


            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var token = await context.RefreshTokens.FirstOrDefaultAsync(e => e.Token == refreshToken);
            if (token == null || token.Expiration < DateTime.UtcNow) return false;
            return true;
        }
    }
}
