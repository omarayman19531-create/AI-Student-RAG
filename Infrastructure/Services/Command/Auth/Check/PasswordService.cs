using Application.Dto;
using Application.Interfaces.Auth;
using Domain.Entity.Auth;
using Domain.Repostry;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Command.Check
{
    public class PasswordService(IUserService userService,UserManager<Appuser>userManager) : IPasswordService
    {
        public async Task<ServiceResponse> ResetPassword(string email, string token, string password)
        {
            var user = await userService.GetUserByEmail(email);
            if (user == null)
            {
                return new ServiceResponse(false, "Not found");
            }
            var result = await userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
                return new ServiceResponse(false, "Invalid token");

            return new ServiceResponse(true, "Password changed");
        }

        public async Task<ServiceResponse> VerifyResetToken(string email, string token)
        {
            var user = await userService.GetUserByEmail(email);
            if(user==null)
            {
                return new ServiceResponse(false,"Not found");
            }
            var isvalid = await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword",
    token);
            if (isvalid == false)
            {
                return new ServiceResponse(false, "Not Valid Token");
            }
            return new ServiceResponse(true, token);

        }
    }

}
