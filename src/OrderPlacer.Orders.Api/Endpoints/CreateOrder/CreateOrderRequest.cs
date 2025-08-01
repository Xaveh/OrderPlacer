namespace OrderPlacer.Orders.Api.Endpoints.CreateOrder;

public record CreateOrderRequest(List<CreateOrderItemRequest> Items);
public record CreateOrderItemRequest(string ProductId, string ProductName, int Quantity, decimal UnitPrice);