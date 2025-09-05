using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class ItemPriceConfiguration
    {
        public void Configure(EntityTypeBuilder<ItemPrice> builder)
        {
            builder.HasKey(e => e.ItemPriceId).HasName("PK__ItemPric__7E70A262E4D5CACD");

            builder.HasOne(d => d.Currency).WithMany(p => p.ItemPrices).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Item).WithMany(p => p.ItemPrices).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
