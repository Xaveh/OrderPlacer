using FastEndpoints;
using MassTransit;
using OrderPlacer.Orders.Api.Consumers;
using OrderPlacer.Orders.Api.Data;
using OrderPlacer.Orders.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddFastEndpoints();
builder.AddNpgsqlDbContext<OrdersDbContext>("order-placer");

builder.Services.AddStackExchangeRedisCache(options => options.Configuration = builder.Configuration.GetConnectionString("redis"));

builder.Services.AddScoped<IOrderCacheService, OrderCacheService>();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<OrderStatusUpdatedConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetConnectionString("rabbitmq"));
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