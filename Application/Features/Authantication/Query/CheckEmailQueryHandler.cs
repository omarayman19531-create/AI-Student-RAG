using Application.Dto;
using Application.Interfaces.Auth;
using Domain.Repostry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Query
{
    public class CheckEmailQueryHandler(IUserService userService,IPasswordService passwordService) : IRequestHandler<CheckEmailQuery, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CheckEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByEmail(request.email);
            if(user== null)
            {
                return new ServiceResponse(false, "User Not Found");
            }
            var check = await passwordService.VerifyResetToken(request.email, request.token);
            return check;
        }
    }
}
