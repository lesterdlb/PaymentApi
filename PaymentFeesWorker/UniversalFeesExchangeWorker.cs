using Data.Services.FeeExchange;

namespace PaymentFeesWorker;

public class UniversalFeesExchangeWorker : BackgroundService
{
    private readonly ILogger<UniversalFeesExchangeWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public UniversalFeesExchangeWorker(
        ILogger<UniversalFeesExchangeWorker> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using var scope = _serviceProvider.CreateScope();
            var feeExchangeService = scope.ServiceProvider.GetRequiredService<IFeeExchangeService>();

            await feeExchangeService.UpdateFee(RandomNumber(0, 2), stoppingToken);

            // 1 Hour
            await Task.Delay(60000, stoppingToken);
        }
    }

    private static decimal RandomNumber(int min, int max)
    {
        return Math.Round((decimal)new Random().NextDouble() * (max - min) + min, 2);
    }
}