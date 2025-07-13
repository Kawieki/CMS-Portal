using Application.DTOs.Articles;
using Application.UseCases.Articles;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly CreateArticle _createArticle;
    private readonly GetArticles _getArticles;
    private readonly GetArticleDetails _getArticleDetails;
    private readonly UpdateArticle _updateArticle;
    private readonly PublishArticle _publishArticle;
    private readonly GetArticleStats _getArticleStats;

    public ArticlesController(
        CreateArticle createArticle,
        GetArticles getArticles,
        GetArticleDetails getArticleDetails,
        UpdateArticle updateArticle,
        PublishArticle publishArticle,
        GetArticleStats getArticleStats)
    {
        _createArticle = createArticle;
        _getArticles = getArticles;
        _getArticleDetails = getArticleDetails;
        _updateArticle = updateArticle;
        _publishArticle = publishArticle;
        _getArticleStats = getArticleStats;
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDto dto)
    {
        var articleId = await _createArticle.HandleAsync(dto);
        return CreatedAtAction(nameof(GetArticle), new { articleId }, new { Id = articleId });
    }

    [HttpGet]
    public async Task<IActionResult> GetArticles([FromQuery] ArticleStatus? status = null)
    {
        var articles = await _getArticles.HandleAsync(status);
        return Ok(articles);
    }

    [HttpGet("{articleId:guid}")]
    public async Task<IActionResult> GetArticle(Guid articleId)
    {
        var article = await _getArticleDetails.HandleAsync(articleId);
        return Ok(article);
    }

    [HttpPut("{articleId:guid}")]
    public async Task<IActionResult> UpdateArticle(Guid articleId, [FromBody] UpdateArticleDto dto)
    {
        await _updateArticle.HandleAsync(articleId, dto);
        return Ok();
    }

    [HttpPost("{articleId:guid}/publish")]
    public async Task<IActionResult> PublishArticle(Guid articleId)
    {
        await _publishArticle.HandleAsync(articleId);
        return Ok();
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _getArticleStats.HandleAsync();
        return Ok(stats);
    }
}