using Domain.Interfaces;
using Moq;
using Application.UseCases.Articles;
using Domain.Entities;
using Domain.Services;

namespace Tests;

public class GetArticleStatsTests
{
    private readonly Mock<IArticleRepository> _mockRepo = new();
    private readonly Mock<ICategoryRepository> _mockCategoryRepo = new();
    private readonly Mock<ArticleStatsService> _mockStatsService = new();

    [Fact]
    public async Task HandleAsync_ShouldReturnCorrectStatistics()
    {
        // Arrange
        var testArticles = new List<Article>
        {
            new(
                "Artykuł 1",
                "Treść artykułu 1",
                "Autor 1",
                "artykul-1"
            ),
            new(
                "Artykuł 2", 
                "Treść artykułu 2",
                "Autor 2",
                "artykul-2"
            ),
            new(
                "Artykuł 3",
                "Treść artykułu 3", 
                "Autor 3",
                "artykul-3"
            ),
            new(
                "Artykuł 4",
                "Treść artykułu 4",
                "Autor 4", 
                "artykul-4"
            )
        };

        testArticles[0].Publish();
        testArticles[1].Publish();
        
        _mockRepo.Setup(r => r.GetAllAsync(null))
                .ReturnsAsync(testArticles);

        var getArticleStats = new GetArticleStats(
            _mockRepo.Object,
            _mockCategoryRepo.Object,
            _mockStatsService.Object
            );

        // Act
        var stats = await getArticleStats.HandleAsync();

        // Assert
        Assert.Equal(2, stats.PublishedCount); // 2 opublikowane artykuły
        Assert.Equal(2, stats.DraftCount); // 2 artykuły w drafcie
    }

    [Fact]
    public async Task HandleAsync_WhenNoArticles_ShouldReturnZeroStatistics()
    {
        // Arrange
       _mockRepo.Setup(r => r.GetAllAsync(null))
                .ReturnsAsync(new List<Article>());

        var getArticleStats = new GetArticleStats(
            _mockRepo.Object,
            _mockCategoryRepo.Object,
            _mockStatsService.Object
            );

        // Act
        var stats = await getArticleStats.HandleAsync();

        // Assert
        Assert.Equal(0, stats.PublishedCount);
        Assert.Equal(0, stats.DraftCount);
    }

    [Fact]
    public async Task HandleAsync_WhenOnlyPublishedArticles_ShouldReturnCorrectCounts()
    {
        // Arrange
        var publishedArticles = new List<Article>
        {
            new(
                "Opublikowany 1",
                "Treść artykułu jest nieznana sorry :<",
                "Autor",
                "opublikowany-1"
            ),
            new(
                "Opublikowany 2",
                "Treść artykułu jest nieznana sorry meeeow :3",
                "Autor", 
                "opublikowany-2"
            )
        };

        publishedArticles[0].Publish();
        publishedArticles[1].Publish();

        _mockRepo.Setup(r => r.GetAllAsync(null))
            .ReturnsAsync(publishedArticles);

        var getArticleStats = new GetArticleStats(
            _mockRepo.Object,
            _mockCategoryRepo.Object,
            _mockStatsService.Object
            );

        // Act
        var stats = await getArticleStats.HandleAsync();

        // Assert
        Assert.Equal(2, stats.PublishedCount);
        Assert.Equal(0, stats.DraftCount);
    }
}