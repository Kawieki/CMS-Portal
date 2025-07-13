using Domain.Interfaces;
using Domain.Services;

namespace Application.UseCases.Articles;

public class GetArticleStats
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ArticleStatsService _statsService;

    public GetArticleStats(
        IArticleRepository articleRepository, 
        ICategoryRepository categoryRepository, 
        ArticleStatsService statsService)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _statsService = statsService;
    }

    public async Task<ArticleStats> HandleAsync()
    {
        var articles = await _articleRepository.GetAllAsync();
        var categories = await _categoryRepository.GetAllAsync();
        
        return _statsService.Calculate(articles, categories);
    }
}