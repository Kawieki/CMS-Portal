using Application.DTOs.Articles;
using Application.Exceptions;
using Application.Validation;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.UseCases.Articles;

public class CreateArticle
{
    private readonly IArticleRepository _articleRepository;
    private readonly IValidator<CreateArticleDto> _validator;

    public CreateArticle(IArticleRepository articleRepository, IValidator<CreateArticleDto> validator)
    {
        _articleRepository = articleRepository;
        _validator = validator;
    }

    public async Task<Guid> HandleAsync(CreateArticleDto dto)
    {
        var errors = await _validator.ValidateAsync(dto);
        if (errors.Count != 0)
            throw new ValidationException(errors);
        
        var slug = SlugGenerator.Generate(dto.Title);
        var counter = 1;
        
        while (await _articleRepository.SlugExistsAsync(slug))
        {
            counter++;
            slug = $"{slug}-{counter}";
        }
        
        var article = new Article(
            dto.Title,
            dto.Content,
            dto.Author,
            slug
        );
        
        await _articleRepository.AddAsync(article);
        return article.Id;
    }
}