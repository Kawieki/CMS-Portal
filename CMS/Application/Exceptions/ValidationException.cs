namespace Application.Exceptions
{
    public class ValidationException(List<string> errors) : Exception("Validation failed")
    {
        public List<string> Errors { get; } = errors;
    }
} 