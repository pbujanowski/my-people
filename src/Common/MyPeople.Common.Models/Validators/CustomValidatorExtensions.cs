using FluentValidation;

namespace MyPeople.Common.Models.Validators;

public static class CustomValidatorExtensions
{
    public static IRuleBuilderOptions<T, Guid?> NotEmptyGuid<T>(this IRuleBuilder<T, Guid?> ruleBuilder)
        => ruleBuilder.SetValidator(new NotEmptyGuidValidator<T>());
}