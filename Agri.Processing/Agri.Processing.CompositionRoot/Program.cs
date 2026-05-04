using Agri.Processing.Inbound.RabbitMq;
using Agri.Processing.Outbound.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddRabbitMqInbound(context.Configuration)
            .AddRabbitMqOutbound()
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Agri.Processing.Application.AssemblyReference.Assembly);
            });
    })
    .Build();

await host.RunAsync();
