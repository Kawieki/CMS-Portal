using Domain.Interfaces;
using Moq;
using Application.DTOs.Categories;
using Application.Validation;

namespace Tests;

public class CategoryValidatorTests
{
    [Fact]
    public async Task ValidateAsync_WhenCategoryNameIsNotUnique_ShouldReturnError()
    {
        // Arrange
        var dto = new CreateCategoryDto { Name = "Technologia" };

        var mockRepo = new Mock<ICategoryRepository>();
        mockRepo.Setup(r => r.ExistsByNameAsync("Technologia"))
            .ReturnsAsync(true);

        var validator = new CategoryValidator(mockRepo.Object);

        // Act
        var errors = await validator.ValidateAsync(dto);

        // Assert
        Assert.Contains("Kategoria o takiej nazwie już istnieje.", errors);
    }

    [Fact]
    public async Task ValidateAsync_WhenCategoryNameIsUnique_ShouldNotReturnError()
    {
        // Arrange
        var dto = new CreateCategoryDto { Name = "NowaKategoria" };

        var mockRepo = new Mock<ICategoryRepository>();
        mockRepo.Setup(r => r.ExistsByNameAsync("NowaKategoria"))
            .ReturnsAsync(false);

        var validator = new CategoryValidator(mockRepo.Object);

        // Act
        var errors = await validator.ValidateAsync(dto);

        // Assert
        Assert.DoesNotContain("Kategoria o takiej nazwie już istnieje.", errors);
        Assert.Empty(errors); 
    }
}