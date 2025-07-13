using Application.DTOs.Articles;
using Application.Exceptions;
using Domain.Interfaces;

namespace Application.UseCases.Articles;

public class GetArticleDetails
{
    private readonly IArticleRepository _articleRepository;

    public GetArticleDetails(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<ArticleDetailsDto> HandleAsync(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id);

        if (article == null)
            throw new NotFoundException($"Artykuł o ID {id} nie istnieje.");
    

        return new ArticleDetailsDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            Author = article.Author,
            Slug = article.Slug,
            Status = article.Status,
            CategoryId = article.CategoryId,
            CreatedAt = article.CreatedAt
        };
    }
}
