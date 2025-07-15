namespace Application.DTOs.Articles;

public class UpdateArticleDto
{
   
    public string Title { get; set; } = null!;
    public string Content { get; set; }  = null!;
    public string Author { get; set; } = null!;
    public Guid? CategoryId { get; set; }
}