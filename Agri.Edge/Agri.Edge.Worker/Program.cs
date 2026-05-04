using Agri.Edge.Worker;
using Agri.Edge.Worker.Generators.Humidity;
using Agri.Edge.Worker.Generators.Ph;
using Agri.Edge.Worker.Generators.Temperature;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.ConfigureDependencies(context.Configuration);

        services.AddMediator(cfg =>
        {
            cfg.AddConsumers(AssemblyReference.Assembly);
        });

        services.AddHostedService<TemperatureGenerator>();
        services.AddHostedService<HumidityGenerator>();
        services.AddHostedService<PhGenerator>();
    })
    .Build();

await host.RunAsync();
