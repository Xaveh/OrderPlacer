var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitmq = builder.AddRabbitMQ("rabbitmq");
var redis = builder.AddRedis("redis");
var postgres = builder.AddPostgres("postgres", port: 5432)
    .WithDataVolume()
    .AddDatabase("order-placer");

// Services
// As for today (2025.08.07.), .NET Aspire service discovery is not compatible with YARP load balancing, so WithReplicas option is not used here.
// See: https://github.com/dotnet/aspire/issues/9486
var ordersApi1 = builder.AddProject<Projects.OrderPlacer_Orders_Api>("orders-api-1")
    .WithReference(postgres)
    .WithReference(rabbitmq)
    .WithReference(redis)
    .WaitFor(postgres)
    .WaitFor(rabbitmq)
    .WaitFor(redis)
    .WithHttpEndpoint(port: 7001, name: "orders-api-1-http");

var ordersApi2 = builder.AddProject<Projects.OrderPlacer_Orders_Api>("orders-api-2")
    .WithReference(postgres)
    .WithReference(rabbitmq)
    .WithReference(redis)
    .WaitFor(postgres)
    .WaitFor(rabbitmq)
    .WaitFor(redis)
    .WithHttpEndpoint(port: 7002, name: "orders-api-2-http");

// External API for fulfillment
var fulfillmentExternalApi = builder
    .AddProject<Projects.OrderPlacer_Fulfillment_ExternalApi>("fulfillment-external-api")
    .WithHttpEndpoint(port: 7003, name: "fulfillment-external-api-http");

builder.AddProject<Projects.OrderPlacer_Fulfillment_Service>("fulfillment-service")
    .WithReference(rabbitmq)
    .WithReference(fulfillmentExternalApi)
    .WaitFor(rabbitmq);

// Gateway
builder.AddProject<Projects.OrderPlacer_Gateway>("gateway")
    .WithReference(ordersApi1)
    .WithReference(ordersApi2)
    .WaitFor(ordersApi1)
    .WaitFor(ordersApi2);

builder.Build().Run();