namespace Application.DTOs.Categories;

public class GetCategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = null!;
}