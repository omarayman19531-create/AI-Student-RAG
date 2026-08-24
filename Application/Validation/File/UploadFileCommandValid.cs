using Application.Features.File;
using FluentValidation;


namespace Application.Validation.File
{
    public class UploadFileCommandValid : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValid()
        {
            RuleFor(x => x.FormFile)
            .NotNull()
            .WithMessage("The file is empty");

            When(x => x.FormFile != null, () =>
            {
                RuleFor(x => x.FormFile.Length)
                    .LessThanOrEqualTo(5 * 1024 * 1024)
                    .WithMessage("File size must be less than or equal to 5 MB");

                RuleFor(x => x.FormFile.FileName)
                    .Must(fileName =>
                        Path.GetExtension(fileName)
                            .Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    .WithMessage("Only PDF files are allowed");
            });
        }
    }
}
