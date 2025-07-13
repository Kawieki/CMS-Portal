namespace Application.Validation;

public interface IValidator<T>
{
    Task<List<string>> ValidateAsync(T obj);
}