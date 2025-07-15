namespace Application.Validation;

public static class ArticleValidationRules
{
    private const int MinContentLength = 10;

    public static List<string> Validate(string? title, string? content, string? author)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(title))
            errors.Add("Tytuł nie może być pusty");

        if (string.IsNullOrWhiteSpace(content))
            errors.Add("Treść nie może być pusta");
        else if (content.Length < MinContentLength)
            errors.Add($"Treść musi mieć co najmniej {MinContentLength} znaków");

        if (string.IsNullOrWhiteSpace(author))
            errors.Add("Autor nie może być pusty");

        return errors;
    }
}
