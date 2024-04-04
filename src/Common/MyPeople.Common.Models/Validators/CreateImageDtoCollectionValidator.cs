using FluentValidation;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Common.Models.Validators;

public class CreateImageDtoCollectionValidator : AbstractValidator<List<CreateImageDto>>
{
    public CreateImageDtoCollectionValidator()
    {
        RuleForEach(x => x)
            .SetValidator(new CreateImageDtoValidator());
    }
}