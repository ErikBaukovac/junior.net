using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class CustomerAddressConfiguration
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.HasKey(e => e.CustomerAddressId).HasName("PK__Customer__DB891B7898EB01F1");

            builder.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
