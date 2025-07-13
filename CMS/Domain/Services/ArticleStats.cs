namespace Domain.Services;

public class ArticleStats
{
    public int PublishedCount { get; set; }
    public int DraftCount { get; set; }
    public string? MostPopularCategory { get; set; }
}