namespace Contracts.Requests;

public record CreditCardResponse(
    Guid Id,
    string CardHolderName,
    string CardNumber,
    string Cvv,
    DateTime ExpirationDate,
    decimal Balance
);