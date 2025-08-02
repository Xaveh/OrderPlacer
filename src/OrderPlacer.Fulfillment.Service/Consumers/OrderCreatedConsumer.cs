using MassTransit;
using OrderPlacer.Contracts;

namespace OrderPlacer.Fulfillment.Service.Consumers;

public sealed class OrderCreatedConsumer(IPublishEndpoint publishEndpoint) : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        // TODO: Handle creation. For now, we just simulate fulfillment.
        await Task.Delay(5000, context.CancellationToken);

        await publishEndpoint.Publish(
            new OrderStatusUpdated(context.Message.OrderId, OrderStatus.Fulfilled, DateTimeOffset.UtcNow),
            context.CancellationToken);
    }
}