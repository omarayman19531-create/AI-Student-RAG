using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Auth
{
    public record RegisterDto
   (string Email,
    string Password,
    string ConfirmPassword,
    string FullName,
    string? PhoneNumber);
}
