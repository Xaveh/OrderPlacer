using System.ComponentModel.DataAnnotations;
using OrderPlacer.Contracts;

namespace OrderPlacer.Orders.Api.Models;

public class Order
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public required string ProductName { get; init; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }

    public OrderStatus Status { get; set; } = OrderStatus.Created;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; set; }
}