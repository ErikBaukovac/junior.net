using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class OrderItemConfiguration
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED0681FCDB47E1");

            builder.HasOne(d => d.ItemPrice).WithMany(p => p.OrderItems).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Order).WithMany(p => p.OrderItems).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
