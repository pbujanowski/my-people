using FluentValidation.Resources;

namespace MyPeople.Common.Models.Validators.Resources;

public class CustomLanguageManager : LanguageManager
{
    public CustomLanguageManager()
    {
        AddTranslation("en", "NotEmptyGuidValidator", "'{PropertyName}' must not be empty GUID.");
        AddTranslation("pl", "NotEmptyGuidValidator", "Pole '{PropertyName}' nie może być pustą wartością GUID.");
    }
}