using Domain.Helpers;
using Domain.Interfaces;

namespace Application.Services;

public interface ISlugService
{
    Task<string> GenerateUniqueSlugAsync(string title);
}

public class SlugService : ISlugService
{
    private readonly IArticleRepository _articleRepository;

    public SlugService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<string> GenerateUniqueSlugAsync(string title)
    {
        var slug = SlugGenerator.Generate(title);
        var counter = 1;
        
        while (await _articleRepository.SlugExistsAsync(slug))
        {
            counter++;
            slug = $"{slug}-{counter}";
        }
        
        return slug;
    }
}