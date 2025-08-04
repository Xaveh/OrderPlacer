using FastEndpoints;
using MassTransit;
using OrderPlacer.Orders.Api.Consumers;
using OrderPlacer.Orders.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddFastEndpoints();
builder.AddCosmosDbContext<OrdersDbContext>("cosmosdb", "order-placer");

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<OrderStatusUpdatedConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        var connectionString = builder.Configuration.GetConnectionString("rabbitmq");
        configurator.Host(connectionString);
        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseFastEndpoints();

app.Run();