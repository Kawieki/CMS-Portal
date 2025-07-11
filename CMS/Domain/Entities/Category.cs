namespace Domain.Entities;

public class Category
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = null!;

    public Category(string name)
    {
        SetName(name);
        Id = Guid.NewGuid();
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        Name = name;
    }
}