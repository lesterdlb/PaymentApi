namespace Data.Models;

public class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}