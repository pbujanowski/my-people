using FluentValidation;
using FluentValidation.Validators;

namespace MyPeople.Common.Models.Validators;

public class NotEmptyGuidValidator<T> : PropertyValidator<T, Guid?>
{
    public override string Name => "NotEmptyGuidValidator";

    public override bool IsValid(ValidationContext<T> context, Guid? value) => value != Guid.Empty;

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        Localized(errorCode, Name);
}
