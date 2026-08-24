using Application.Dto;
using Application.Interfaces.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.ForgetPassowerd
{
    public class ForgetPassowedrCommandHandler(IEmailService emailService) : IRequestHandler<ForgetPassowerdCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(ForgetPassowerdCommand request, CancellationToken cancellationToken)
        {
            var message = await emailService.SendEmailAsync(request.email); 
            return message;
        }
    }
}
