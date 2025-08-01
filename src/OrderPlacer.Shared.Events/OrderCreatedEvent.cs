namespace OrderPlacer.Shared.Events;

public record OrderCreatedEvent(
    string OrderId,
    List<OrderItemEvent> Items,
    decimal TotalAmount,
    DateTime CreatedAt
);

public record OrderItemEvent(
    string ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);