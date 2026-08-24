using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.Login
{
    public record LoginCommand
    (
        string Email,
        string Password

        ) : IRequest<LoginResponce>;
}
