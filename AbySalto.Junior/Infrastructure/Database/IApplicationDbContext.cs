using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Infrastructure.Database
{
    public interface IApplicationDbContext
    {
        DbSet<Currency> Currencies { get; }
        DbSet<Customer> Customers { get; }
        DbSet<CustomerAddress> CustomerAddresses { get; }
        DbSet<Item> Items { get; }
        DbSet<ItemPrice> ItemPrices { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<OrderStatus> OrderStatuses { get; }
        DbSet<PaymentType> PaymentTypes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
