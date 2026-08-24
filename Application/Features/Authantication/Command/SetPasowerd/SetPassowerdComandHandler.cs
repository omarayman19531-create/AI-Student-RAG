using Application.Dto;
using Application.Interfaces.Auth;
using Application.Validation;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.SetPasowerd
{
    public class SetPassowerdComandHandler(IPasswordService passwordService,IvalidationService ivalidationService,IValidator<SetPassowerdComand>validator) : IRequestHandler<SetPassowerdComand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(SetPassowerdComand request, CancellationToken cancellationToken)
        {
           var valid=await ivalidationService.validationservice(request, validator);
            if (!valid.Success)
            {
                string error = string.Join(", ", valid.Message);
                return new ServiceResponse(false, error);
            }

         var newpass=await passwordService.ResetPassword(request.email, request.token, request.passowerd);
            return newpass;
        }
    }
}
