namespace OrderPlacer.Contracts;

public record OrderStatusUpdated(string OrderId, OrderStatus Status, DateTimeOffset UpdatedAt);