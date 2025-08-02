using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderPlacer.Contracts;
using OrderPlacer.Orders.Api.Data;

namespace OrderPlacer.Orders.Api.Consumers;

public class OrderStatusUpdatedConsumer(OrdersDbContext dbContext) : IConsumer<OrderStatusUpdated>
{
    public async Task Consume(ConsumeContext<OrderStatusUpdated> context)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == context.Message.OrderId);

        if (order is null)
        {
            // TODO: Handle the case where the order is not found.
            return;
        }

        order.Status = context.Message.Status;
        order.UpdatedAt = context.Message.UpdatedAt;

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(context.CancellationToken);
    }
}