using Contracts.Requests;
using ErrorOr;

namespace Data.Services.CreditCards;

public interface ICreditCardService
{
    Task<ErrorOr<CreditCardResponse>> Get(Guid id);
    Task<CreditCardResponse> Create(CreditCardRequest createCardRequest);
}