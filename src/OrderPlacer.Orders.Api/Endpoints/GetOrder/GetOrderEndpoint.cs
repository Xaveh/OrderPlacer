using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using OrderPlacer.Orders.Api.Data;
using OrderPlacer.Orders.Api.Endpoints.CreateOrder;

namespace OrderPlacer.Orders.Api.Endpoints.GetOrder;

public class GetOrderEndpoint(OrdersDbContext dbContext) : Endpoint<GetOrderRequest, GetOrderResponse>
{
    public override void Configure()
    {
        Get("/orders/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (order == null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        await Send.OkAsync(new GetOrderResponse(
            order.Id,
            order.Items.Select(i => new GetOrderItemResponse(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList(),
            order.TotalAmount,
            order.Status,
            order.CreatedAt
        ), cancellationToken);
    }
}