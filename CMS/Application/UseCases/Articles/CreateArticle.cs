using Application.DTOs.Articles;
using Application.Exceptions;
using Application.Services;
using Application.Validation;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.UseCases.Articles;

public class CreateArticle
{
    private readonly IArticleRepository _articleRepository;
    private readonly IValidator<CreateArticleDto> _validator;
    private readonly ISlugService _slugService;

    public CreateArticle(
        IArticleRepository articleRepository, 
        ISlugService slugService,
        IValidator<CreateArticleDto> validator)
    {
        _articleRepository = articleRepository;
        _validator = validator;
        _slugService = slugService;
    }

    public async Task<Guid> HandleAsync(CreateArticleDto dto)
    {
        var errors = await _validator.ValidateAsync(dto);
        if (errors.Count != 0)
            throw new ValidationException(errors);
        
        var slug = await _slugService.GenerateUniqueSlugAsync(dto.Title);
        
        var article = new Article(dto.Title, dto.Content, dto.Author, slug);
        await _articleRepository.AddAsync(article);
        return article.Id;
        
    }
}