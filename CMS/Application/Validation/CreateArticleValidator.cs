using Application.DTOs.Articles;

namespace Application.Validation;

public class CreateArticleValidator : IValidator<CreateArticleDto>
{
    public Task<List<string>> ValidateAsync(CreateArticleDto dto)
    {
        var errors = ArticleValidationRules.Validate(dto.Title, dto.Content, dto.Author);
        return Task.FromResult(errors);
    }
}