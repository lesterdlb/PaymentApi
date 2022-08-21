using Data.Context;
using Data.Models;

namespace Data.Services.FeeExchange;

public class FeeExchangeService : IFeeExchangeService
{
    private readonly DatabaseContext _context;
    private const string PaymentFee = "PAYMENT_FEE";

    public FeeExchangeService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> UpdateFee(decimal randomNumber, CancellationToken stoppingToken)
    {
        var entity = _context.Params.FirstOrDefault(p => p.Key == PaymentFee);
        if (entity is null)
        {
            _context.Params.Add(new Param
            {
                Key = PaymentFee,
                Value = randomNumber
            });
        }
        else
        {
            var newFee = Math.Round(entity.Value * randomNumber, 2);
            if (newFee == 0)
                newFee = randomNumber;
            
            entity.Value = newFee;
        }
        
        return await _context.SaveChangesAsync(stoppingToken);
    }
}