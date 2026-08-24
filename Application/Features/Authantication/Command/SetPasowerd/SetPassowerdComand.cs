using Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.SetPasowerd
{
    public record SetPassowerdComand
    (
        string email,string token, string passowerd, string ConfirmPassword) :IRequest<ServiceResponse>;
    
}
