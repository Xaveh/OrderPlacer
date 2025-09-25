using MassTransit;
using OrderPlacer.Fulfillment.Service.Consumers;
using OrderPlacer.Fulfillment.Service.Services;
using Polly;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddServiceDiscovery();

builder.Services.AddHttpClient<IExternalFulfillmentService, ExternalFulfillmentService>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:7003");
    })
    .AddServiceDiscovery()
    .AddStandardResilienceHandler(options =>
    {
        // Customize the retry policy
        options.Retry.MaxRetryAttempts = 3;
        options.Retry.Delay = TimeSpan.FromSeconds(2);
        options.Retry.BackoffType = DelayBackoffType.Constant;

        // Customize the circuit breaker policy
        options.CircuitBreaker.FailureRatio = 0.5;
        options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(20);
        options.CircuitBreaker.MinimumThroughput = 2;
        options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(60);

        // Customize the timeout policy
        options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(10);
        options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(60);
    });

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