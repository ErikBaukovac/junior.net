using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class OrderConfiguration
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF9936EC4B");

            builder.HasOne(d => d.Currency).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CustomerAddress).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.OrderStatus).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.PaymentType).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
