using Application.DTOs.Categories;
using Domain.Interfaces;

namespace Application.Validation;

public class CategoryValidator : IValidator<CreateCategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<string>> ValidateAsync(CreateCategoryDto dto)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Nazwa nie może być pusta.");
        if (await _categoryRepository.ExistsByNameAsync(dto.Name))
            errors.Add("Kategoria o takiej nazwie już istnieje.");
        return errors;
    }
}