using FastEndpoints;
using OrderPlacer.Orders.Api.Data;
using OrderPlacer.Orders.Api.Models;
using Order = OrderPlacer.Orders.Api.Models.Order;

namespace OrderPlacer.Orders.Api.Endpoints.CreateOrder;

public class CreateOrderEndpoint : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
    public override void Configure()
    {
        Post("/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
    {
        var dbContext = Resolve<OrdersDbContext>();

        var order = new Order
        {
            Items = req.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(ct);

        await Send.OkAsync(new CreateOrderResponse(
            order.Id,
            order.Items,
            order.TotalAmount,
            order.Status,
            order.CreatedAt
        ), ct);
    }
}