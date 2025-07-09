using Domain.Entities;

namespace Tests;

public class ArticleTests
{
    [Fact]
    public void SetTitle_ShouldUpdatesSlugCorrectly()
    {
        // Arrange
        var article = new Article("Start", "kontent z conajmniej 10 znakami", "Autor"); 
        
        // Act
        article.SetTitle("Nowy tytuł \n");
        
        // Assert
        Assert.Equal("nowy-tytul", article.Slug);
    }

    [Fact]
    public void SetTitle_ShouldThrowArgumentException_WhenTitleIsNull()
    {
        // Arrange
        var article = new Article("Start", "kontent z conajmniej 10 znakami", "Autor");
        
        // Act + Assert
        Assert.Throws<ArgumentException>(() => article.SetTitle(null));
    }

    [Fact]
    public void SetTitle_ShouldThrowArgumentException_WhenTitleIsEmpty()
    {
        // Arrange
        var article = new Article("Start", "kontent z conajmniej 10 znakami", "Autor");
        
        // Act + Assert
        Assert.Throws<ArgumentException>(() => article.SetTitle(""));
    }

    [Fact]
    public void SetTitle_ShouldThrowArgumentException_WhenTitleIsWhitespace()
    {
        // Arrange
        var article = new Article("Start", "kontent z conajmniej 10 znakami", "Autor");
        
        // Act + Assert
        Assert.Throws<ArgumentException>(() => article.SetTitle("   "));
    }
}