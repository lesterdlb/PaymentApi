namespace Data.Services.FeeExchange;

public interface IFeeExchangeService
{
    Task<int> UpdateFee(decimal randomNumber, CancellationToken stoppingToken);
}