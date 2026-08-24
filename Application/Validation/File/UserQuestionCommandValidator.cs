using Application.Features.question;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.File
{
    public class UserQuestionCommandValidator:AbstractValidator<UserQuestionCommand>
    {
        public UserQuestionCommandValidator()
        {
            RuleFor(x => x.question)
                .NotEmpty()
                .WithMessage("Question is required.")
                .MaximumLength(1000)
                .WithMessage("Question is too long.");
        }
    }
}
