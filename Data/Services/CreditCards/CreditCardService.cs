using Data.Context;
using Data.Models;
using Contracts.Requests;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.CreditCards;

public class CreditCardService : ICreditCardService
{
    private readonly DatabaseContext _context;

    public CreditCardService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<CreditCardResponse>> Get(Guid id)
    {
        var card = await _context.CreditCards.SingleOrDefaultAsync(t => t.Id == id);

        return card is not null
            ? new CreditCardResponse(
                card.Id,
                card.CardHolderName,
                card.CardNumber,
                card.Cvv,
                card.ExpirationDate,
                card.Balance
            )
            : Error.NotFound("Card not found.");
    }

    public async Task<CreditCardResponse> Create(CreditCardRequest createCardRequest)
    {
        var card = await _context.CreditCards.AddAsync(new CreditCard
        {
            Id = Guid.NewGuid(),
            CardHolderName = createCardRequest.CardHolderName,
            CardNumber = createCardRequest.CardNumber,
            ExpirationDate = createCardRequest.ExpirationDate,
            Cvv = createCardRequest.Cvv,
            Balance = createCardRequest.Balance
        });
        await _context.SaveChangesAsync();

        return new CreditCardResponse(
            card.Entity.Id,
            card.Entity.CardHolderName,
            card.Entity.CardNumber,
            card.Entity.Cvv,
            card.Entity.ExpirationDate,
            card.Entity.Balance
        );
    }
}