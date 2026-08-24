using Application.Dto;
using Application.Dto.Auth;
using Application.Features.Authantication.Command.Login;
using Application.Interfaces.Auth;
using Domain.Entity.Auth;
using Domain.Repostry;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Command.Auth
{
    public class AuthService(AppDbContext context,UserManager<Appuser>userManager,ITokenService tokenService,IUserService userService) : IAuthService
    {
        public async Task<ServiceResponse> Register(RegisterDto register)
        {
            var user = await userService.GetUserByEmail(register.Email);
            if(user!=null)
            {
                return new ServiceResponse(false, "The Account is Exist Should choose another email");
            }
            var app = new Appuser
            {
                Email = register.Email,
                UserName= register.Email,
                FullName= register.FullName,
                PhoneNumber=register.PhoneNumber,
                
               
                
                
            };
         var create=  await userManager.CreateAsync(app,register.Password);
            if(!create.Succeeded)
            {
                string error = string.Join(", ", create.Errors.Select(x => x.Description));
                return new ServiceResponse(false, error);
            }
           var role= await userManager.AddToRoleAsync(app, "User");

            if (!role.Succeeded)
            {
                var error = string.Join(", ", role.Errors.Select(x => x.Description));
                return new ServiceResponse(false, error);
            }
            return new ServiceResponse(true, "Create Account Is Success");
        }



        public async Task<LoginResponce> Login(LoginDto login)
        {

            var user = await userService.GetUserByEmail(login.Email);

            if (user == null)
            {
                return new LoginResponce(false,"","", "Invalid email or password.");
            }
          
            var result = await userManager.CheckPasswordAsync(user, login.Password);
            if(result==false)
            {
                return new LoginResponce(false,"","", "The Email or Passowerd is incorrect");
            }
            var claims = await tokenService.GetUserClaims(login.Email);
            var token = tokenService.GenerateToken(claims);
            var refreshtoken = tokenService.GenerateRefreshToken();
            var save=await tokenService.AddRefreshToken(user.Id, refreshtoken);
            if(save==false)
            {
                return new LoginResponce(false, "",""," InCorrect in refreshtoken");
            }
            return  new LoginResponce(true,token,refreshtoken, "Success");
        }

        public async Task<LoginResponce> ReviveRefreshToken(string refreshToken)
        {
            var valid = await tokenService.ValidateRefreshToken(refreshToken);
            if(valid==false)
            {
                return new LoginResponce(false,"","", "Not Valid");
            }
            var userid = await tokenService.GetUserByRefreshToken(refreshToken);

            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return new LoginResponce(false, "","","Not found");
            }
            var NewRefreshToken = tokenService.GenerateRefreshToken();
            var claims=await tokenService.GetUserClaims(userid);
            var token = tokenService.GenerateToken(claims);
            var update = await tokenService.UpdateRefreshToken(refreshToken, NewRefreshToken);
            if(update==false)
            {
                return new LoginResponce(false,"","", "Failed Update");
            }
            return new LoginResponce(true, token,NewRefreshToken,"Update Is Success");

        }
    }
}
