using Application.Features.Authantication.Command.SetPasowerd;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.auth
{
    public class SetNewPassowerdvalid:AbstractValidator<SetPassowerdComand>
    {
        public SetNewPassowerdvalid()
        {

            RuleFor(x => x.passowerd)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");
            RuleFor(x => x.ConfirmPassword)
               .NotEmpty()
               .WithMessage("Confirm Password is required")
               .Equal(x => x.passowerd)
               .WithMessage("Password and Confirm Password do not match");
        }
    }
}
