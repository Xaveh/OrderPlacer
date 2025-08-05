using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using OrderPlacer.Orders.Api.Endpoints.GetOrder;

namespace OrderPlacer.Orders.Api.Services;

public class OrderCacheService(IDistributedCache cache) : IOrderCacheService
{
    private static readonly TimeSpan DefaultCacheExpiration = TimeSpan.FromMinutes(5);
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<GetOrderResponse?> GetOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var cacheKey = GetCacheKey(orderId);
        var cachedOrder = await cache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(cachedOrder))
        {
            return null;
        }

        return JsonSerializer.Deserialize<GetOrderResponse>(cachedOrder, JsonOptions);
    }

    public async Task SetOrderAsync(GetOrderResponse order, CancellationToken cancellationToken = default)
    {
        var cacheKey = GetCacheKey(order.Id);
        var serializedOrder = JsonSerializer.Serialize(order, JsonOptions);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = DefaultCacheExpiration
        };

        await cache.SetStringAsync(cacheKey, serializedOrder, options, cancellationToken);
    }

    public async Task InvalidateOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var cacheKey = GetCacheKey(orderId);
        await cache.RemoveAsync(cacheKey, cancellationToken);
    }

    private static string GetCacheKey(Guid orderId) => $"order:{orderId}";
}
