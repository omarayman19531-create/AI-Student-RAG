using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        Task<bool> AddRefreshToken(string userid, string refreshToken);
        Task<string?> GetUserByRefreshToken(string refreshToken);
        Task<List<Claim>> GetUserClaims(string email);
        Task<bool>UpdateRefreshToken(string oldrefreshtoken, string newrefreshToken);
        Task<bool> ValidateRefreshToken(string refreshToken);
    }
}
