using Domain.Enums;
using Domain.Helpers;

namespace Domain.Entities;

public class Article
{
    public Guid Id { get; init; }
    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public string Author { get; private set; } = null!; 
    public string Slug { get; private set; } = null!;
    public ArticleStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Article(string title, string content, string author)
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        SetContent(content);
        SetAuthor(author);
        CreatedAt = DateTime.UtcNow;
        Status = ArticleStatus.Draft;
    }
    
    public void Publish()
    {
        Status = ArticleStatus.Published;
    }
    
    public void SetAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        Author = author;
    }
    
    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        Title = title;
        Slug = SlugGenerator.Generate(title);
    }

    public void SetContent(string content)
    {
        if (content == null || content.Length < 10)
            throw new ArgumentException("Content must have at least 10 characters.");
        Content = content;
    }
}