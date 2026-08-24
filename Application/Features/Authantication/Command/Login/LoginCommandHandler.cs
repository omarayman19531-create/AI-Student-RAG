using Application.Dto.Auth;
using Application.Interfaces.Auth;
using Application.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.Login
{
    public class LoginCommandHandler(IMapper mapper,IvalidationService ivalidation,IValidator<LoginCommand>validator,IAuthService authService) : IRequestHandler<LoginCommand, LoginResponce>
    {
        public async Task<LoginResponce> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var valid = await ivalidation.validationservice(request, validator);
            if(!valid.Success)
            {
                string error = string.Join(", ", valid.Message);
                return new LoginResponce (false,null,null, error);
            }
            var map = mapper.Map<LoginDto>(request);
            var result = await authService.Login(map);
            if(!result.Success)
            {
                return new LoginResponce(false,null, null, result.Message);
            }
            return result;
        }
    }
}
