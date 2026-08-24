using Domain.Entity.Auth;
using Domain.Repostry;
using Microsoft.AspNetCore.Identity;
using Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserService(UserManager<Appuser> userManager) : IUserService
    {
        public async Task<Appuser?> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
    }
}
