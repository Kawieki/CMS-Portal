using Application.DTOs.Articles;
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
        var article = await _articleRepository.GetDetailsByIdAsync(id);

        return new ArticleDetailsDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            Author = article.Author,
            Slug = article.Slug,
            CategoryId = article.CategoryId,
            CreatedAt = article.CreatedAt
        };
    }
}
