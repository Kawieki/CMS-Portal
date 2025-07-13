using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly CmsDbContext _dbContext;
    
    public ArticleRepository(CmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Article article)
    {
        await _dbContext.Articles.AddAsync(article);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Article>> GetAllAsync(ArticleStatus? status = null)
    {
        var query = _dbContext.Articles.AsQueryable();
        
        if (status.HasValue)
            query = query.Where(a => a.Status == status.Value);
            
        return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public Task<Article?> GetByIdAsync(Guid id)
    {
        var article = _dbContext.Articles
            .FirstOrDefaultAsync(a => a.Id == id);
        return article;
    }

    public async Task UpdateAsync(Article article)
    {
        _dbContext.Articles.Update(article);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task PublishAsync(Article article)
    {
        article.Publish();
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> SlugExistsAsync(string slug)
    {
        return await _dbContext.Articles.AnyAsync(a => a.Slug == slug);
    }

}