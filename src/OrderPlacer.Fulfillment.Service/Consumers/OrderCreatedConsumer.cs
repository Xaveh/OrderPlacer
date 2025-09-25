using MassTransit;
using OrderPlacer.Contracts;
using OrderPlacer.Fulfillment.Service.Services;

namespace OrderPlacer.Fulfillment.Service.Consumers;

public sealed class OrderCreatedConsumer(
    IPublishEndpoint publishEndpoint,
    IExternalFulfillmentService externalFulfillmentService)
    : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var success =
            await externalFulfillmentService.FulfillOrderAsync(context.Message.OrderId, context.CancellationToken);

        var status = success ? OrderStatus.Fulfilled : OrderStatus.Failed;

        await publishEndpoint.Publish(
            new OrderStatusUpdated(context.Message.OrderId, status, DateTimeOffset.UtcNow),
            context.CancellationToken);
    }
}