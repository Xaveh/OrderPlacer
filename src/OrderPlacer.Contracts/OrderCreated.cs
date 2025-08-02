namespace OrderPlacer.Contracts;

public record OrderCreated(string OrderId, string ProductName, int Quantity, DateTimeOffset CreatedAt);