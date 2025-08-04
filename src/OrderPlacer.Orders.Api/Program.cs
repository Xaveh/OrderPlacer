using FastEndpoints;
using MassTransit;
using OrderPlacer.Orders.Api.Consumers;
using OrderPlacer.Orders.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddFastEndpoints();
builder.AddNpgsqlDbContext<OrdersDbContext>("order-placer");

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

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();