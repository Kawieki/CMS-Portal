using Domain.Entities;
using Domain.Enums;

namespace Domain.Services;

public class ArticleStatsService
{
    public ArticleStats Calculate(IEnumerable<Article> articles, IEnumerable<Category> categories)
    {
        var articlesData = articles.ToList();
        var published = articlesData.Count(a => a.Status == ArticleStatus.Published);
        var draft = articlesData.Count(a => a.Status == ArticleStatus.Draft);
        
        var mostPopularCategoryId = articlesData
            .Where(a => a.CategoryId != null)
            .GroupBy(a => a.CategoryId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();

        var mostPopularCategory = categories
            .FirstOrDefault(c => c.Id == mostPopularCategoryId)?.Name;

        return new ArticleStats
        {
            PublishedCount = published,
            DraftCount = draft,
            MostPopularCategory = mostPopularCategory
        };
    }
}