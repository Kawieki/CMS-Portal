using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Articles;

public class CreateArticleDto
{
    [Required]
    public string Title { get;  set; } = null!;
    
    [Required]
    [MinLength(10)]
    public string Content { get;  set; } = null!;
    
    [Required]
    public string Author { get;  set; } = null!; 
    
    public Guid CategoryId { get; set; }
}