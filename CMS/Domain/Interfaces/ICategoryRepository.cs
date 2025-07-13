using Domain.Entities;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task AddAsync(Category category);
    Task<bool> ExistsByNameAsync(string name);
}