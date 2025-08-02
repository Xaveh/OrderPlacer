using FastEndpoints;
using MassTransit;
using OrderPlacer.Orders.Api.Data;
using Microsoft.EntityFrameworkCore;
using OrderPlacer.Orders.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseCosmos(builder.Configuration.GetConnectionString("CosmosDB")!, "OrdersDatabase"));

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<OrderStatusUpdatedConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]!);
            h.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseFastEndpoints();

app.Run();