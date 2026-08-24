using Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.ForgetPassowerd
{
    public record ForgetPassowerdCommand
    (string email):IRequest<ServiceResponse>;
}
