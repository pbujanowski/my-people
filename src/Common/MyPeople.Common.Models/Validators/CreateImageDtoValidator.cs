using FluentValidation;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Common.Models.Validators;

public class CreateImageDtoValidator : AbstractValidator<CreateImageDto>
{
    public CreateImageDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256);

        RuleFor(x => x.Content).NotEmpty();

        RuleFor(x => x.ContentType).NotEmpty();

        RuleFor(x => x.PostId).NotEmpty();
    }
}
