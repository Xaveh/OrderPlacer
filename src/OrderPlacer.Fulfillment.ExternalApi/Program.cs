var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapPost("/api/fulfillment", async (FulfillmentRequest request) =>
{
    // Simulate processing time
    await Task.Delay(2000);

    return Results.Ok(new { success = true, orderId = request.OrderId });
});

app.Run();

public record FulfillmentRequest(Guid OrderId);