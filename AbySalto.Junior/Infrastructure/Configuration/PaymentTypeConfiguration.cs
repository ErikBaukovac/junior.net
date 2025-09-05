using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Configuration
{
    public class PaymentTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(e => e.PaymentTypeId).HasName("PK__PaymentT__BA430B357E19F81A");
        }
    }
}
