namespace OrderPlacer.Orders.Api.Models;

public class Order
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public List<OrderItem> Items { get; init; } = [];

    public decimal TotalAmount => Items.Sum(x => x.Quantity * x.UnitPrice);

    public OrderStatus Status { get; init; } = OrderStatus.Created;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;

    public DateTimeOffset? UpdatedAt { get; init; }
}