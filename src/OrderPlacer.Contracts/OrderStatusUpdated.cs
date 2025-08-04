namespace OrderPlacer.Contracts;

public record OrderStatusUpdated(Guid OrderId, OrderStatus Status, DateTimeOffset UpdatedAt);