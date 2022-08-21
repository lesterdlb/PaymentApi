using Data.Context;
using Data.Models;
using Contracts.Requests;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly DatabaseContext _context;

    public TransactionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<TransactionResponse>> Add(TransactionRequest addTransactionRequest)
    {
        var card = await _context.CreditCards.FindAsync(addTransactionRequest.CardId);

        if (card == null) return Error.NotFound("Card not found.");

        if (card.Balance < addTransactionRequest.Amount)
            return Error.Conflict("Insufficient funds.");

        var fee = (await _context.Params.FirstOrDefaultAsync(p => p.Key == "PAYMENT_FEE"))?.Value ?? 1;

        card.Balance -= addTransactionRequest.Amount + fee;

        var transaction = await _context.Transactions.AddAsync(new Transaction
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.Date,
            Amount = addTransactionRequest.Amount,
            Fee = fee,
            Total = addTransactionRequest.Amount + fee,
            CreditCard = card
        });
        await _context.SaveChangesAsync();

        return new TransactionResponse(
            transaction.Entity.Id,
            transaction.Entity.Date,
            transaction.Entity.CreditCard.CardNumber,
            transaction.Entity.Amount,
            transaction.Entity.Fee,
            transaction.Entity.Total
        );
    }
}