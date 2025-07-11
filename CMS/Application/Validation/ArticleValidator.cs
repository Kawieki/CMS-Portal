using Application.DTOs.Articles;

namespace Application.Validation;

public class ArticleValidator : IValidator<CreateArticleDto>
{
    private const int MinContentLength = 10;
    
    public async Task<List<string>> ValidateAsync(CreateArticleDto item)
    {
        var errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(item.Title))
            errors.Add("Title cannot be empty");
            
        if (string.IsNullOrWhiteSpace(item.Content))
            errors.Add("Content cannot be empty");
        else if (item.Content.Length < MinContentLength)
            errors.Add($"Content must be at least {MinContentLength} characters long");
            
        if (string.IsNullOrWhiteSpace(item.Author))
            errors.Add("Author cannot be empty");
            
        return await Task.FromResult(errors);
    }
}