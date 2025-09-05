using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class ItemConfiguration
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(e => e.ItemId).HasName("PK__Item__727E838B3DCD4BE2");
        }
    }
}
