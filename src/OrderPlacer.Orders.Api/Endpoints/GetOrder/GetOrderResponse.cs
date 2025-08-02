using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Endpoints.GetOrder;

public record GetOrderResponse(
    string Id,
    string ProductName,
    int Quantity,
    OrderStatus Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);