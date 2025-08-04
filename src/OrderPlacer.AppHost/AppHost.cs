var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitmq = builder.AddRabbitMQ("rabbitmq");
var cosmosdb = builder.AddAzureCosmosDB("cosmosdb")
    .RunAsEmulator()
    .AddCosmosDatabase("order-placer");

// Services
var ordersApi = builder.AddProject<Projects.OrderPlacer_Orders_Api>("orders-api")
    .WithReference(cosmosdb)
    .WaitFor(cosmosdb)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

var fulfillmentService = builder.AddProject<Projects.OrderPlacer_Fulfillment_Service>("fulfillment-service")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

// Gateway
builder.AddProject<Projects.OrderPlacer_Gateway>("gateway")
    .WithReference(ordersApi)
    .WaitFor(ordersApi);

builder.Build().Run();