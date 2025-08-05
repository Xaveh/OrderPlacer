using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using OrderPlacer.Orders.Api.Data;
using OrderPlacer.Orders.Api.Services;

namespace OrderPlacer.Orders.Api.Endpoints.GetOrder;

public class GetOrderEndpoint(OrdersDbContext dbContext, IOrderCacheService cacheService) : Endpoint<GetOrderRequest, GetOrderResponse>
{
    public override void Configure()
    {
        Get("/orders/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrderRequest request, CancellationToken cancellationToken)
    {
        var cachedOrder = await cacheService.GetOrderAsync(request.Id, cancellationToken);
        if (cachedOrder is not null)
        {
            await Send.OkAsync(cachedOrder, cancellationToken);
            return;
        }

        var order = await dbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (order is null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        var orderResponse = new GetOrderResponse(
            order.Id,
            order.ProductName,
            order.Quantity,
            order.Status,
            order.CreatedAt,
            order.UpdatedAt
        );

        await cacheService.SetOrderAsync(orderResponse, cancellationToken);

        await Send.OkAsync(orderResponse, cancellationToken);
    }
}