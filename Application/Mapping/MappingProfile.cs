using Application.Dto.Auth;
using Application.Features.Authantication.Command.Login;
using Application.Features.Authantication.Command.Register;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterCommand, RegisterDto>();
            CreateMap<LoginCommand, LoginDto>();

        }
    }
}
