namespace OrderPlacer.Contracts;

public record OrderCreated(Guid OrderId, string ProductName, int Quantity, DateTimeOffset CreatedAt);