namespace Application.DTOs.Articles;

public class ArticleListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
}