namespace OrderPlacer.Shared.Events;

public record OrderProcessingEvent(
    string OrderId,
    DateTime ProcessingStartedAt
);