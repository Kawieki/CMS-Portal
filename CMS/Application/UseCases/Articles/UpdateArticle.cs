using Application.DTOs.Articles;
using Application.Exceptions;
using Application.Validation;
using Domain.Interfaces;

namespace Application.UseCases.Articles;

public class UpdateArticle
{
    private readonly IArticleRepository _articleRepository;
    private readonly IValidator<UpdateArticleDto> _articleValidator;
    
    public UpdateArticle(IArticleRepository articleRepository, IValidator<UpdateArticleDto> articleValidator)
    {
        _articleRepository = articleRepository;
        _articleValidator = articleValidator;
    }
    
    public async Task HandleAsync(Guid id, UpdateArticleDto dto)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        article.Update(dto.Title, dto.Content, dto.Author, dto.CategoryId);
        
        var errors = await _articleValidator.ValidateAsync(dto);
        if (errors.Count != 0)
            throw new ValidationException(errors);
        
        await _articleRepository.UpdateAsync(article);
    }
}