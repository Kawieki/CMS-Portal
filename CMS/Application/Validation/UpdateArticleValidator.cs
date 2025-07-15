using Application.DTOs.Articles;

namespace Application.Validation;

public class UpdateArticleValidator : IValidator<UpdateArticleDto>
{
    public Task<List<string>> ValidateAsync(UpdateArticleDto dto)
    {
        var errors = ArticleValidationRules.Validate(dto.Title, dto.Content, dto.Author);
        return Task.FromResult(errors);
    }
}