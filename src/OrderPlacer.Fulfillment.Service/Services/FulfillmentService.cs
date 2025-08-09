using System.Text.Json;

namespace OrderPlacer.Fulfillment.Service.Services;

public interface IExternalFulfillmentService
{
    Task<bool> FulfillOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}

public sealed class ExternalFulfillmentService(HttpClient httpClient) : IExternalFulfillmentService
{
    public async Task<bool> FulfillOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var request = new { OrderId = orderId };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/api/fulfillment", content, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}