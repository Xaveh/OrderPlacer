using Microsoft.EntityFrameworkCore;
using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Data;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.OwnsMany(e => e.Items, item =>
            {
                item.Property(i => i.ProductId).IsRequired();
                item.Property(i => i.ProductName).IsRequired();
                item.Property(i => i.Quantity).IsRequired();
                item.Property(i => i.UnitPrice);
            });
        });
    }
}