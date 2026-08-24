using Application.Dto;
using Application.Dto.Auth;
using Application.Interfaces.Auth;
using Application.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.Register
{
    public class RegisterCommandHandler(IvalidationService ivalidation,IValidator<RegisterCommand> validator,IAuthService authService,IMapper mapper) : IRequestHandler<RegisterCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var valid = await ivalidation.validationservice(request, validator);
            if(!valid.Success)
            {
                string error=string.Join(", ",valid.Message);
                return new ServiceResponse(false,error);
            }
            var datamapping = mapper.Map<RegisterDto>(request);
            var x = await authService.Register(datamapping);
            if(!x.Success)
            {
                return new ServiceResponse(false, "Faild to create account");
            }
            return new ServiceResponse(true, "Success to create account");

        }
    }
}
