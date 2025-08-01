using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Endpoints.CreateOrder;

public record CreateOrderResponse(string Id, List<OrderItem> Items, decimal TotalAmount, OrderStatus Status, DateTimeOffset CreatedAt);