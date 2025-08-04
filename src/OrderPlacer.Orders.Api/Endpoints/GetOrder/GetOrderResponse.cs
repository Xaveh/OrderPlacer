using OrderPlacer.Contracts;

namespace OrderPlacer.Orders.Api.Endpoints.GetOrder;

public record GetOrderResponse(
    Guid Id,
    string ProductName,
    int Quantity,
    OrderStatus Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);