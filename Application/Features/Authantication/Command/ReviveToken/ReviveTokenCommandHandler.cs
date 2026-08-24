using Application.Features.Authantication.Command.Login;
using Application.Interfaces.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.ReviveToken
{
    public class ReviveTokenCommandHandler(IAuthService authService) : IRequestHandler<ReviveTokenCommand, LoginResponce>
    {
        public async Task<LoginResponce> Handle(ReviveTokenCommand request, CancellationToken cancellationToken)
        {
            var update =await authService.ReviveRefreshToken(request.RefreshToken);
            return update;
        }
    }
}
