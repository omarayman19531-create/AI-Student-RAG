using Application.Dto;
using Application.Dto.Auth;
using Application.Features.Authantication.Command.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse> Register(RegisterDto register);
        Task<LoginResponce> Login(LoginDto login);
        Task<LoginResponce> ReviveRefreshToken(string refreshToken);

    }
}
