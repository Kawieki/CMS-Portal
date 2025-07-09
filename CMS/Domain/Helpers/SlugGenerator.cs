using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Helpers;

public static class SlugGenerator
{
    private static readonly Dictionary<char, char> PolishCharMap = new()
    {
        ['ł'] = 'l',
        ['ś'] = 's',
        ['ź'] = 'z',
        ['ż'] = 'z',
        ['ć'] = 'c',
        ['ń'] = 'n',
        ['ą'] = 'a',
        ['ę'] = 'e',
    };

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

    // Usuwa znaki diakrytyczne w oparciu o dekompozycję Unicode.
    // Obsługuje wiele języków, choć nie wszystkie przypadki specjalne (np. ß -> ss).
    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        
        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                continue;

            sb.Append(PolishCharMap.TryGetValue(c, out var mapped) ? mapped : c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}