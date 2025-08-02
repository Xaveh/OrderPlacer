using FastEndpoints;
using OrderPlacer.Orders.Api.Data;
using Order = OrderPlacer.Orders.Api.Models.Order;

namespace OrderPlacer.Orders.Api.Endpoints.CreateOrder;

public class CreateOrderEndpoint(OrdersDbContext dbContext) : Endpoint<CreateOrderRequest>
{
    public override void Configure()
    {
        Post("/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            ProductName = request.ProductName,
            Quantity = request.Quantity,
        };

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        await Send.OkAsync(cancellation: cancellationToken);
    }
}