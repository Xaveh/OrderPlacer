var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitmq = builder.AddRabbitMQ("rabbitmq");
var redis = builder.AddRedis("redis");
var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .AddDatabase("order-placer");

// Services
var ordersApi = builder.AddProject<Projects.OrderPlacer_Orders_Api>("orders-api")
    .WithReference(postgres)
    .WaitFor(postgres)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithReference(redis)
    .WaitFor(redis);

var fulfillmentService = builder.AddProject<Projects.OrderPlacer_Fulfillment_Service>("fulfillment-service")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

// Gateway
builder.AddProject<Projects.OrderPlacer_Gateway>("gateway")
    .WithReference(ordersApi)
    .WaitFor(ordersApi);

builder.Build().Run();