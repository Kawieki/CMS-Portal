namespace Application.DTOs.Articles;

public class UpdateArticleDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public Guid? CategoryId { get; set; }
}