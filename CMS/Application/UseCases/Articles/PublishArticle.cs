using Application.Exceptions;
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
            throw new NotFoundException($"Artykuł o ID {id} nie istnieje.");

        if (article.Status != ArticleStatus.Draft)
            throw new ArticleAlreadyPublishedException(id);

        await _articleRepository.PublishAsync(article);
    }
}