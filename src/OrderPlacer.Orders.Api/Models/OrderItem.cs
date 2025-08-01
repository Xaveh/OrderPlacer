using System.ComponentModel.DataAnnotations;

namespace OrderPlacer.Orders.Api.Models;

public class OrderItem
{
    public required string ProductId { get; init; } 
    
    public required string ProductName { get; init; }
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }
    
    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; init; }
}