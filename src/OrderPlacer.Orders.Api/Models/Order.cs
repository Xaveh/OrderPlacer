using System.ComponentModel.DataAnnotations;
using OrderPlacer.Contracts;

namespace OrderPlacer.Orders.Api.Models;

public class Order
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public required string ProductName { get; init; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }

    public OrderStatus Status { get; set; } = OrderStatus.Created;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;

    public DateTimeOffset? UpdatedAt { get; set; }
}