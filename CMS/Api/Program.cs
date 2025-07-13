using Application.DTOs.Articles;
using Application.DTOs.Categories;
using Application.Exceptions;
using Application.Services;
using Application.UseCases.Articles;
using Application.UseCases.Categories;
using Application.Validation;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<CmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Services
builder.Services.AddScoped<ArticleStatsService>();
// builder.Services.AddScoped<IArticleStatsService, ArticleStatsService>();
builder.Services.AddScoped<ISlugService, SlugService>();

// Validators
builder.Services.AddScoped<IValidator<CreateArticleDto>, CreateArticleValidator>();
builder.Services.AddScoped<IValidator<UpdateArticleDto>, UpdateArticleValidator>();
builder.Services.AddScoped<IValidator<CreateCategoryDto>, CategoryValidator>();

// Use Cases
builder.Services.AddScoped<CreateArticle>();
builder.Services.AddScoped<GetArticles>();
builder.Services.AddScoped<GetArticleDetails>();
builder.Services.AddScoped<UpdateArticle>();
builder.Services.AddScoped<PublishArticle>();
builder.Services.AddScoped<GetArticleStats>();
builder.Services.AddScoped<CreateCategory>();
builder.Services.AddScoped<GetCategories>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { errors = validationException.Errors });
                break;

            case NotFoundException notFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { message = notFoundException.Message });
                break;

            case ArticleAlreadyPublishedException articleAlreadyPublished:
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsJsonAsync(new { message = articleAlreadyPublished.Message });
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { message = "Wystąpił nieoczekiwany błąd serwera." });
                break;
        }
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<CmsDbContext>();
    await DbSeeder.SeedAsync(dbContext);
}

app.Run();