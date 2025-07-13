using Application.DTOs.Articles;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.UseCases.Articles;

public class GetArticles
{
    private readonly IArticleRepository _articleRepository;
    
    public GetArticles(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    
    public async Task<IEnumerable<ArticleListDto>> HandleAsync(ArticleStatus? status = null)
    {
        var articles = await _articleRepository.GetAllAsync(status);
        
        
        return articles.Select(article => new ArticleListDto
        {
            Id = article.Id,
            Title = article.Title,
            Slug = article.Slug,
        });
    }
    
}