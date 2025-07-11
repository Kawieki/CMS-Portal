using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Categories;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; } = null!;
}