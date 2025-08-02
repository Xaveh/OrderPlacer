namespace OrderPlacer.Orders.Api.Endpoints.CreateOrder;

public record CreateOrderRequest(string ProductName, int Quantity);