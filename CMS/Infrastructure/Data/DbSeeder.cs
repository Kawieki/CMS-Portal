using Domain.Entities;
using Domain.Helpers;

namespace Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(CmsDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category ("News") ,
                new Category ( "Tutorials"),
                new Category ( "Opinion")
            );
            await context.SaveChangesAsync();
        }
        
        if (!context.Articles.Any())
        {
            var articles = new List<Article>
            {
                new(
                    title: "Witamy w naszym CMS",
                    content: "To jest pierwszy artykuł w naszym systemie!",
                    author: "Admin",
                    slug: SlugGenerator.Generate("Witamy w naszym CMS")
                ),
                new(
                    title: "Jak korzystać z naszego CMS",
                    content: "Krok po kroku jak efektywnie korzystać z naszego systemu.",
                    author: "Redaktor",
                    slug: SlugGenerator.Generate("Jak korzystać z naszego CMS")
                ),
                new(
                    title: "Najnowsze wiadomości z branży",
                    content: "Oto najnowsze aktualności ze świata technologii.",
                    author: "Reporter",
                    slug: SlugGenerator.Generate("Najnowsze wiadomości z branży")
                ),
                new(
                    title: "Opinia: Dlaczego CMS jest ważny",
                    content: "Moim zdaniem CMS to klucz do nowoczesnego zarządzania treścią.",
                    author: "Współpracownik",
                    slug: SlugGenerator.Generate("Opinia: Dlaczego CMS jest ważny")
                ),
                new(
                    title: "Top 10 platform CMS w 2025 roku",
                    content: "Ranking najlepszych platform CMS dostępnych obecnie na rynku.",
                    author: "Analityk",
                    slug: SlugGenerator.Generate("Top 10 platform CMS w 2025 roku")
                )
            };

            articles[0].CategoryId = context.Categories.First(c => c.Name == "News").Id;
            articles[2].Publish();
            articles[3].Publish();
            
            context.Articles.AddRange(articles);
            await context.SaveChangesAsync();
        }
    }
}
