using Domain.Enums;

namespace Application.DTOs.Articles;

public class ArticleDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public ArticleStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CategoryId { get; set; }
}