using Application.Features.Authantication.Command.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.auth
{
    public class RegisterValid: AbstractValidator<RegisterCommand>
    {
        public RegisterValid()
        {
            RuleFor(x => x.Email)
                 .NotEmpty()
                 .WithMessage("Email is required")
                 .EmailAddress()
                 .WithMessage("Email format is not valid");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required")
                .Equal(x => x.Password)
                .WithMessage("Password and Confirm Password do not match");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required")
                .MinimumLength(3)
                .WithMessage("Full name must be at least 3 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^[0-9]+$")
                .WithMessage("Phone number must contain only numbers");
        }
    }
}
