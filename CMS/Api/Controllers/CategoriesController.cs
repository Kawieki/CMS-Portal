using Application.DTOs.Categories;
using Application.UseCases.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategory _createCategory;
    private readonly GetCategories _getCategories;

    public CategoriesController(CreateCategory createCategory, GetCategories getCategories)
    {
        _createCategory = createCategory;
        _getCategories = getCategories;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _getCategories.HandleAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        await _createCategory.HandleAsync(dto);
        return Ok();
    }
}