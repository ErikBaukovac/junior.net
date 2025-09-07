using AbySalto.Junior.Application.DTO;
using AbySalto.Junior.Application.Interfaces;
using AbySalto.Junior.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Application.Services
{
    public class OrderService : IRestaurantService
    {
        private readonly IApplicationDbContext _context;

        public OrderService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderModel>> GetOrders()
        {

            var orders = await _context.Orders.Select(o => new OrderModel
                {
                    OrderId = o.OrderId,
                    CustomerName = o.CustomerAddress.Customer.CustomerName,
                    OrderStatus = o.OrderStatus.StatusName,
                    CreatedAt = o.CreatedAt,
                    PaymentType = o.PaymentType.PaymentName,
                    Address = o.CustomerAddress.StreetName 
                    + " " + o.CustomerAddress.BuildingNo 
                    + (string.IsNullOrEmpty(o.CustomerAddress.ApartmentNo) ? "" : "/" + o.CustomerAddress.ApartmentNo) 
                    + ", " + o.CustomerAddress.PostCode + " " + o.CustomerAddress.City,
                    PhoneNumber = o.CustomerAddress.Customer.PhoneNumber,
                    Comments = o.Comments,
                    Items = o.OrderItems.Select(oi => new OrderItemModel
                    {
                        ItemName = oi.ItemPrice.Item.ItemName,
                        Quantity = oi.Quantity,
                        Price = oi.ItemPrice.Price
                    }).ToList(),
                    TotalValue = o.TotalValue,


                }).ToListAsync();

            return orders;
        }
    }
}
