using Application.DTOs.Articles;
using Application.UseCases.Articles;
using Application.Validation;
using Application.Services;
using Application.Exceptions;
using Domain.Interfaces;
using Moq;

namespace Tests;

public class CreateArticleTests
{
    private readonly CreateArticle _createArticle;
    private readonly Mock<IValidator<CreateArticleDto>> _mockValidator;

    public CreateArticleTests()
    {
        var mockRepository = new Mock<IArticleRepository>();
        var mockSlugService = new Mock<ISlugService>();
        _mockValidator = new Mock<IValidator<CreateArticleDto>>();

        _createArticle = new CreateArticle(
            mockRepository.Object, 
            mockSlugService.Object,
            _mockValidator.Object
        );
    }

    [Fact]
    public async Task HandleAsync_WhenTitleIsEmpty_ShouldThrowValidationException()
    {
        // Arrange
        var dto = new CreateArticleDto
        {
            Title = "",
            Content = "To jest treść artykułu, która ma więcej niż 10 znaków.",
            Author = "Jan Kowalski",
            CategoryId = Guid.NewGuid()
        };
        
        _mockValidator
            .Setup(v => v.ValidateAsync(dto))
            .ReturnsAsync(["Tytuł nie może być pusty."]);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => 
            _createArticle.HandleAsync(dto));
    }
}