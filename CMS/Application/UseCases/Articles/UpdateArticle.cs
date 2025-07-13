using Application.DTOs.Articles;
using Application.Exceptions;
using Application.Services;
using Application.Validation;
using Domain.Interfaces;

namespace Application.UseCases.Articles;

public class UpdateArticle
{
    private readonly IArticleRepository _articleRepository;
    private readonly IValidator<UpdateArticleDto> _articleValidator;
    private readonly ISlugService _slugService;
    
    public UpdateArticle(
        IArticleRepository articleRepository,
        IValidator<UpdateArticleDto> articleValidator,
        ISlugService slugService)
    {
        _articleRepository = articleRepository;
        _articleValidator = articleValidator;
        _slugService = slugService;
    }
    
    public async Task HandleAsync(Guid id, UpdateArticleDto dto)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        
        if (article is null)
            throw new NotFoundException($"Artykuł o id {id} nie został znaleziony");
        
        var errors = await _articleValidator.ValidateAsync(dto);
        
        if (errors.Count != 0)
            throw new ValidationException(errors);
        
        string? newSlug = null;
        
        if (dto.Title != null && dto.Title != article.Title)
        {
            newSlug = await _slugService.GenerateUniqueSlugAsync(dto.Title);
        }
       
        article.Update(
            dto.Title,
            dto.Content,
            dto.Author,
            dto.CategoryId,
            newSlug ?? article.Slug
        );
        await _articleRepository.UpdateAsync(article);
    }
}