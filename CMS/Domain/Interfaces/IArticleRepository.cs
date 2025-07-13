using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(Guid id);
    Task<IEnumerable<Article>> GetAllAsync(ArticleStatus? status = null);
    Task<bool> SlugExistsAsync(string slug);
    Task AddAsync(Article article);
    Task UpdateAsync(Article article);
    Task PublishAsync(Article article);
}
