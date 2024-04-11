using FluentValidation;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Common.Models.Validators;

public class DeleteImageDtoCollectionValidator : AbstractValidator<List<DeleteImageDto>>
{
    public DeleteImageDtoCollectionValidator()
    {
        RuleForEach(x => x).SetValidator(new DeleteImageDtoValidator());
    }
}
