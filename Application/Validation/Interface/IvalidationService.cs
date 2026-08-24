using Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public interface IvalidationService
    {
        Task<ServiceResponse> validationservice<T>(T moddel, IValidator<T> validator);
    }
}
