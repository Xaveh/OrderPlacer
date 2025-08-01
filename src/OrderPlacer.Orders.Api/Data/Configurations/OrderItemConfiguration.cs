using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(i => i.ProductId)
            .IsRequired()
            .HasMaxLength(36);

        builder.Property(i => i.ProductName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Quantity)
            .IsRequired();

        builder.Property(i => i.UnitPrice)
            .IsRequired();
    }
}