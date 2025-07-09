using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Helpers;

public static class SlugGenerator
{
    public static string Generate(string title)
    {
        var slug = RemoveDiacritics(title);
        slug = slug.ToLower();
        slug = Regex.Replace(slug, @"\s+", "-");
        slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");
        slug = Regex.Replace(slug, "-+", "-");
        slug = slug.Trim('-');
        return slug;
    }
    
    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}