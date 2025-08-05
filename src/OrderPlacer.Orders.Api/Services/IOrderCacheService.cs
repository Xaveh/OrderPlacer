using OrderPlacer.Orders.Api.Endpoints.GetOrder;

namespace OrderPlacer.Orders.Api.Services;

public interface IOrderCacheService
{
    Task<GetOrderResponse?> GetOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task SetOrderAsync(GetOrderResponse order, CancellationToken cancellationToken = default);
    Task InvalidateOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}
