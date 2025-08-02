using Microsoft.EntityFrameworkCore;
using OrderPlacer.Orders.Api.Data.Configurations;
using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Data;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
    }
}