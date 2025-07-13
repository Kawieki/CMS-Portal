using Application.DTOs.Articles;

namespace Application.Validation;

public class UpdateArticleValidator : IValidator<UpdateArticleDto>
{
    private const int MinContentLength = 10;
    
    public async Task<List<string>> ValidateAsync(UpdateArticleDto dto)
    { 
        var errors = new List<string>();
        if (dto.Content is { Length: < MinContentLength })
            errors.Add($"Content must be at least {MinContentLength} characters long");
            
        return await Task.FromResult(errors);
    }
}