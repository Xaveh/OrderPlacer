using MassTransit;
using OrderPlacer.Fulfillment.Service.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<OrderCreatedConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        var connectionString = builder.Configuration.GetConnectionString("rabbitmq");
        configurator.Host(connectionString);
        configurator.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();