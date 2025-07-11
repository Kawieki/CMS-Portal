using Application.DTOs.Categories;
using Application.Exceptions;
using Application.Validation;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Categories;

public class CreateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateCategoryDto> _categoryValidator;
    
    public CreateCategory(ICategoryRepository categoryRepository, IValidator<CreateCategoryDto> categoryValidator)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
    }
    
    public async Task HandleAsync(CreateCategoryDto dto)
    {
        var errors = await _categoryValidator.ValidateAsync(dto);
        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }

        var category = new Category(dto.Name);
        await _categoryRepository.AddAsync(category);
    }
}