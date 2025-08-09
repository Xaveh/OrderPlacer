using MassTransit;
using OrderPlacer.Fulfillment.Service.Consumers;
using OrderPlacer.Fulfillment.Service.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddServiceDiscovery();

builder.Services.AddHttpClient<IExternalFulfillmentService, ExternalFulfillmentService>(client =>
    {
        client.BaseAddress = new Uri("http://fulfillment-external-api");
    })
    .AddServiceDiscovery();

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