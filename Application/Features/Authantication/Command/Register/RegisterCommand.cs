//using Application.Authantication.Command;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Authantication.Command.Register
{
    public record RegisterCommand(
       string Email,
    string Password,
    string ConfirmPassword,
    string FullName,
    string PhoneNumber)
    : IRequest<ServiceResponse>;

}
