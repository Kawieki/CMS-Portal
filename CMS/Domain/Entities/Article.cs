using Domain.Enums;

namespace Domain.Entities;

public class Article
{
    public Guid Id { get; init; }
    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public string Author { get; private set; } = null!; 
    public string Slug { get; private set; }
    public ArticleStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CategoryId { get; set; }
    private const int MinContentLength = 10;
    
    public Article(string title, string content, string author, string slug)
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        SetContent(content);
        SetAuthor(author);
        Slug = slug;
        CreatedAt = DateTime.UtcNow;
        Status = ArticleStatus.Draft;
    }
    
    public void Update(string? title, string? content, string? author, Guid? categoryId)
    {
        if (!string.IsNullOrWhiteSpace(title))
            SetTitle(title);

        if (!string.IsNullOrWhiteSpace(content))
            SetContent(content);

        if (!string.IsNullOrWhiteSpace(author))
            SetAuthor(author);

        if (categoryId.HasValue)
            SetCategory(categoryId.Value);
    }

    public void Publish()
    {
        Status = ArticleStatus.Published;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        Title = title;
    }

    private void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty.");
        if(content.Length < MinContentLength)
            throw new ArgumentException($"Content must be at least {MinContentLength} characters long.", nameof(content));
        Content = content;
    }

    private void SetAuthor(string author)
    {
        Author = author;
    }

    private void SetCategory(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}