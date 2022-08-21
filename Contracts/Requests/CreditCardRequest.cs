namespace Contracts.Requests;

public record CreditCardRequest(
    string CardNumber,
    string CardHolderName,
    DateTime ExpirationDate,
    string Cvv,
    decimal Balance
);