using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class OrderStatusConfiguration
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(e => e.OrderStatusId).HasName("PK__OrderSta__BC674CA135FB463B");
        }
    }
}
