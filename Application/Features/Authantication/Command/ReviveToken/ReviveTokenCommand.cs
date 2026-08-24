using Application.Features.Authantication.Command.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.ReviveToken
{
    public record ReviveTokenCommand
 (string RefreshToken) : IRequest<LoginResponce>;
}
