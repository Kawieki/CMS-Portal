using Domain.Enums;
using Domain.Interfaces;

namespace Application.UseCases.Articles;

public class PublishArticle
{
    private readonly IArticleRepository _articleRepository;
    
    public PublishArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    
    public async Task HandleAsync(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null)
        {
            throw new Exception("Article not found");
        }

        if (article.Status != ArticleStatus.Draft)
        {
            throw new Exception("Article is not in draft status");
        }

        await _articleRepository.PublishAsync(id);
    }
}