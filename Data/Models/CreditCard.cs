namespace Data.Models;

public class CreditCard
{
    public Guid Id { get; init; }
    public string CardHolderName { get; init; } = null!;
    public string CardNumber { get; init; } = null!;
    public string Cvv { get; init; } = null!;
    public DateTime ExpirationDate { get; init; }
    public decimal Balance { get; set; }
}