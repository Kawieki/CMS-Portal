namespace Application.Exceptions;

public class ArticleAlreadyPublishedException(Guid articleId)
    : Exception($"Artykuł o ID {articleId} jest już opublikowany.");