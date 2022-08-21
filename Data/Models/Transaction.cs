namespace Data.Models;

public sealed class Transaction
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public decimal Fee { get; init; }
    public decimal Total { get; init; }
    public CreditCard CreditCard { get; init; } = null!;
}