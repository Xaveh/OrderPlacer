var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var redis = builder.AddRedis("redis");
var rabbitmq = builder.AddRabbitMQ("rabbitmq");
var cosmosdb = builder.AddAzureCosmosDB("cosmosdb");

// Services
var ordersApi = builder.AddProject<Projects.OrderPlacer_Orders_Api>("orders-api")
    .WithReference(cosmosdb)
    .WithReference(redis)
    .WithReference(rabbitmq);

var fulfillmentService = builder.AddProject<Projects.OrderPlacer_Fulfillment_Service>("fulfillment-service")
    .WithReference(rabbitmq);

// Gateway
builder.AddProject<Projects.OrderPlacer_Gateway>("gateway")
    .WithReference(ordersApi);

builder.Build().Run();