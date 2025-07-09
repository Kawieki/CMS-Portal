using Domain.Helpers;

namespace Tests;

public class SlugGeneratorTests
{
    [Theory]
    [InlineData("Mój pierwszy artykuł", "moj-pierwszy-artykul")]
    [InlineData("  Tytuł z  wieloma   spacjami  ", "tytul-z-wieloma-spacjami")]
    [InlineData("Tytuł! Z? Zn@kami #Specjalnymi.", "tytul-z-znkami-specjalnymi")]
    [InlineData("Łódź - piękne miasto", "lodz-piekne-miasto")]
    [InlineData("Śląsk & Polska", "slask-polska")]
    [InlineData("123 Test 456", "123-test-456")]
    public void Generate_ShouldReturnCorrectSlug(string title, string expectedResult)
    {
        // Act
        var result = SlugGenerator.Generate(title);
        
        // Assert
        Assert.Equal(expectedResult, result);
    }
}