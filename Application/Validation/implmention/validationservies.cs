using Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.implmention
{
    public class validationservies : IvalidationService
    {
        public async Task<ServiceResponse> validationservice<T>(T model, IValidator<T> validator)
        {
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                string errorjoin = string.Join(";", errors);
                return new ServiceResponse(false, errorjoin);

            }
            return new ServiceResponse(true, "clean");

        }
    }
}
