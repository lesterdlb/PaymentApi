using Data.Services.FeeExchange;
using PaymentFeesWorker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IFeeExchangeService, FeeExchangeService>();
        services.AddHostedService<UniversalFeesExchangeWorker>();
    })
    .Build();

await host.RunAsync();