using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auth
{
    public interface IPasswordService
    {
        Task<ServiceResponse> VerifyResetToken(string email, string token);
        Task<ServiceResponse> ResetPassword(
        string email,
        string token,
        string password);
    }
}
