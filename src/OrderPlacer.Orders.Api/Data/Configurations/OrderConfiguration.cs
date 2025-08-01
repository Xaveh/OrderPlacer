using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderPlacer.Orders.Api.Models;

namespace OrderPlacer.Orders.Api.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasMaxLength(36);
        
        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(40);
        
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        
        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);
        
        builder.OwnsMany(e => e.Items);

        builder.Ignore(e => e.TotalAmount);
    }
}