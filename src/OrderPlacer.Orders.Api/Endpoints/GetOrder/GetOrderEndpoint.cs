using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using OrderPlacer.Orders.Api.Data;

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
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (order is null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        await Send.OkAsync(new GetOrderResponse(
            order.Id,
            order.ProductName,
            order.Quantity,
            order.Status,
            order.CreatedAt,
            order.UpdatedAt
        ), cancellationToken);
    }
}