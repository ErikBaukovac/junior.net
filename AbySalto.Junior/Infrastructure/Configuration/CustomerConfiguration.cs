using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class CustomerConfiguration
    {
        public void Configure(EntityTypeBuilder<Customer> builder)

        {
            builder.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8C16D2170");
        }
    }
}
