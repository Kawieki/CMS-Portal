using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Categories;

public class GetCategories
{
    private readonly ICategoryRepository _categoryRepository;
    
    public GetCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<IEnumerable<Category>> HandleAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }
}