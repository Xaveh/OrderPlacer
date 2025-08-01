using System.ComponentModel.DataAnnotations;

namespace OrderPlacer.Orders.Api.Models;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    public List<OrderItem> Items { get; set; } = [];
    
    public decimal TotalAmount => Items.Sum(x => x.Quantity * x.UnitPrice);
    
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}

public class OrderItem
{
    [Required]
    public string ProductId { get; set; } = string.Empty;
    
    [Required]
    public string ProductName { get; set; } = string.Empty;
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}

public enum OrderStatus
{
    Created = 1,
    Processing = 2,
    Fulfilled = 3,
    Cancelled = 4
}