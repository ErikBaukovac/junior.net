using AbySalto.Junior.Application.DTO;
using AbySalto.Junior.Application.Interfaces;
using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
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
        public async Task<int> AddOrderAsync(OrderModel orderModel)
        {
            //Dohvatiti ID preko cookies ili sessiona
            int customerID = GetCutomerId(orderModel.CustomerName, orderModel.PhoneNumber!);

            int addressId = await GetOrCreateAddressAsync(orderModel, customerID);

            int paymentType = GetPaymentType(orderModel);

            int currency = GetCurrency(orderModel);

            decimal totalValue = CalculateValue(orderModel.Items);

            var order = new Order
            {
                CustomerAddressId = addressId,
                PaymentTypeId = paymentType,
                OrderStatusId = 1,
                CurrencyId = currency,
                TotalValue = totalValue,
                Comments = orderModel.Comments,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.OrderId;
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
                    CustomerStreet = o.CustomerAddress.StreetName,
                    CustomerCity = o.CustomerAddress.City,
                    BuildingNo = o.CustomerAddress.BuildingNo,
                    ApartmentNo = o.CustomerAddress.ApartmentNo,
                    PostCode = o.CustomerAddress.PostCode,
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

        public async Task<bool> ChangeOrderStatus(int orderId, int statusId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order is null)
                return false;

            var statusExists = await _context.OrderStatuses.AnyAsync(s => s.OrderStatusId == statusId);
            if (!statusExists)
                return false;

            order.OrderStatusId = statusId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderModel>> SortOrdersByValue()
        {

            var orders = await _context.Orders
                .OrderByDescending(o => o.TotalValue)
                .Select(o => new OrderModel
                {
                    OrderId = o.OrderId,
                    CustomerName = o.CustomerAddress.Customer.CustomerName,
                    OrderStatus = o.OrderStatus.StatusName,
                    CreatedAt = o.CreatedAt,
                    PaymentType = o.PaymentType.PaymentName,
                    CustomerStreet = o.CustomerAddress.StreetName,
                    CustomerCity = o.CustomerAddress.City,
                    BuildingNo = o.CustomerAddress.BuildingNo,
                    ApartmentNo = o.CustomerAddress.ApartmentNo,
                    PostCode = o.CustomerAddress.PostCode,
                    PhoneNumber = o.CustomerAddress.Customer.PhoneNumber,
                    Comments = o.Comments,
                    Items = o.OrderItems.Select(oi => new OrderItemModel
                    {
                        ItemName = oi.ItemPrice.Item.ItemName,
                        Quantity = oi.Quantity,
                        Price = oi.ItemPrice.Price
                    }).ToList(),
                    TotalValue = o.TotalValue,
                })
                .ToListAsync();

            return orders;
        }

        private int GetCutomerId(string customerName, string phoneNumber)
        {
            var customerID = _context.Customers
               .Where(c => c.CustomerName == customerName && c.PhoneNumber == phoneNumber)
               .Select(c => c.CustomerId)
               .FirstOrDefault();

            return customerID;
        }

        private async Task<int> GetOrCreateAddressAsync(OrderModel orderModel, int customerId)
        {

            var address = _context.CustomerAddresses
                .Where(a => a.CustomerId == customerId &&
                            a.StreetName == orderModel.CustomerStreet &&
                            a.BuildingNo == orderModel.BuildingNo &&
                            a.ApartmentNo == orderModel.ApartmentNo &&
                            a.PostCode == orderModel.PostCode &&
                            a.City == orderModel.CustomerCity)
                .Select(a => a.CustomerAddressId)
                .FirstOrDefault();

            if (address == 0)
            {
                var newAddress = new CustomerAddress
                {
                    CustomerId = customerId,
                    StreetName = orderModel.CustomerStreet,
                    BuildingNo = orderModel.BuildingNo,
                    ApartmentNo = orderModel.ApartmentNo,
                    PostCode = orderModel.PostCode,
                    City = orderModel.CustomerCity
                };

                _context.CustomerAddresses.Add(newAddress);
                await _context.SaveChangesAsync();

                address = newAddress.CustomerAddressId;
            }

            return address;
        }
        private int GetPaymentType(OrderModel orderModel)
        {
            var paymentType = _context.PaymentTypes.FirstOrDefault(p => p.PaymentName == orderModel.PaymentType);

            if (paymentType == null)
            {
                throw new InvalidOperationException($"Payment type '{orderModel.PaymentType}' not found.");
            }

            return paymentType.PaymentTypeId;
        }

        private int GetCurrency(OrderModel orderModel)
        {
            var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyName == orderModel.Currency);

            if (currency == null)
            {
                throw new InvalidOperationException($"Currency '{orderModel.Currency}' not found.");
            }

            return currency.CurrencyId;
        }

        private decimal CalculateValue(List<OrderItemModel> items)
        {
            return items.Sum(item => item.Quantity * item.Price ) ;
        }
        
    }
}
