namespace Contracts.Requests;

public record TransactionRequest(
    Guid CardId,
    DateTime Date,
    decimal Amount
);