using Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Query
{
    public record CheckEmailQuery
    (
        string email,
        string token
        
        ):IRequest<ServiceResponse>;
}
