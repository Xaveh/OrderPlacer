using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using OrderPlacer.Orders.Api.Data;

namespace OrderPlacer.Orders.Api.Endpoints;

public record GetOrderRequest(string Id);

public class GetOrderEndpoint : Endpoint<GetOrderRequest, CreateOrderResponse>
{
    public override void Configure()
    {
        Get("/orders/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrderRequest req, CancellationToken ct)
    {
        var dbContext = Resolve<OrdersDbContext>();

        var order = await dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == req.Id, ct);

        if (order == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(new CreateOrderResponse(
            order.Id,
            order.Items,
            order.TotalAmount,
            order.Status,
            order.CreatedAt
        ), ct);
    }
}