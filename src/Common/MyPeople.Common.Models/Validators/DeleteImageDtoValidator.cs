using FluentValidation;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Common.Models.Validators;

public class DeleteImageDtoValidator : AbstractValidator<DeleteImageDto>
{
    public DeleteImageDtoValidator()
    {
        RuleFor(x => x.Id).NotEmptyGuid();
    }
}
