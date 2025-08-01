using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Endpoints.GetOrder;

public record GetOrderResponse(string Id, List<OrderItem> Items, decimal TotalAmount, OrderStatus Status, DateTimeOffset CreatedAt);