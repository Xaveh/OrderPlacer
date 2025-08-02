using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Endpoints.GetOrder;

public record GetOrderResponse(string Id, List<GetOrderItemResponse> Items, decimal TotalAmount, OrderStatus Status, DateTimeOffset CreatedAt);

public record GetOrderItemResponse(string ProductId, string ProductName, int Quantity, decimal UnitPrice);