namespace Contracts.Requests;

public record TransactionResponse(
    Guid Id,
    DateTime Date,
    string CardNumber,
    decimal Amount,
    decimal Fee,
    decimal Total
);