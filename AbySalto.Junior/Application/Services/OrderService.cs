using AbySalto.Junior.Application.DTO;
using AbySalto.Junior.Application.Interfaces;
using AbySalto.Junior.Domain.Enums;
using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            try
            {
                //Dohvatiti ID preko cookies ili sessiona inače

                if (orderModel.CustomerName.IsNullOrEmpty() || orderModel.PhoneNumber.IsNullOrEmpty())
                    throw new ArgumentException("Customer name and phone number are required.");
                int customerID = await GetCustomerId(orderModel.CustomerName, orderModel.PhoneNumber);
               
                int addressId = await GetOrCreateAddressAsync(orderModel, customerID);
                int paymentType = GetPaymentType(orderModel);
                int currency = GetCurrency(orderModel);
                decimal totalValue = CalculateValue(orderModel.Items);

                var order = new Order
                {
                    CustomerAddressId = addressId,
                    PaymentTypeId = paymentType,
                    OrderStatusId = (int)OrderStatusEnum.NaCekanju,
                    CurrencyId = currency,
                    TotalValue = totalValue,
                    Comments = orderModel.Comments,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return order.OrderId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<OrderModel>> GetOrders()
        {
            return await GetOrdersQuery().ToListAsync();
        }

        public async Task<bool> ChangeOrderStatus(int orderId, int statusId)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing order status: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<OrderModel>> SortOrdersByValue()
        {
            try
            {
                return await GetOrdersQuery()
                    .OrderByDescending(o => o.TotalValue)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sorting orders: {ex.Message}");
                throw;
            }
        }

        public async Task<decimal> GetTotalOrdersValue(int userId)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.CustomerAddress.CustomerId == userId)
                    .SumAsync(o => o.TotalValue ?? 0m);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating total order value: {ex.Message}");
                throw;
            }
        }

        private IQueryable<OrderModel> GetOrdersQuery()
        {
            try
            {

                return _context.Orders.Select(o => new OrderModel
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
                    TotalValue = o.TotalValue
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving orders: {ex.Message}");
                throw;
            }
        }

        private async Task<int> GetCustomerId(string customerName, string phoneNumber)
        {
            try
            {
                var customerID = await _context.Customers
                   .Where(c => c.CustomerName == customerName && c.PhoneNumber == phoneNumber)
                   .Select(c => c.CustomerId)
                   .FirstOrDefaultAsync();

                if (customerID == 0)
                    throw new InvalidOperationException("Customer not found.");

                return customerID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customer ID: {ex.Message}");
                throw;
            }

        }

        private async Task<int> GetOrCreateAddressAsync(OrderModel orderModel, int customerId)
        {
            try
            {
                var address = await _context.CustomerAddresses
                    .Where(a => a.CustomerId == customerId &&
                                a.StreetName == orderModel.CustomerStreet &&
                                a.BuildingNo == orderModel.BuildingNo &&
                                a.ApartmentNo == orderModel.ApartmentNo &&
                                a.PostCode == orderModel.PostCode &&
                                a.City == orderModel.CustomerCity)
                    .Select(a => a.CustomerAddressId)
                    .FirstOrDefaultAsync();

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

                    await _context.CustomerAddresses.AddAsync(newAddress);
                    await _context.SaveChangesAsync();

                    address = newAddress.CustomerAddressId;
                }

                return address;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving or creating address: {ex.Message}");
                throw;
            }
        }
        private int GetPaymentType(OrderModel orderModel)
        {
            try
            {
                var paymentType = _context.PaymentTypes.FirstOrDefault(p => p.PaymentName == orderModel.PaymentType);

                if (paymentType == null)
                {
                    throw new InvalidOperationException($"Payment type '{orderModel.PaymentType}' not found.");
                }

                return paymentType.PaymentTypeId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving payment type: {ex.Message}");
                throw;
            }
        }

        private int GetCurrency(OrderModel orderModel)
        {
            try
            {
                var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyName == orderModel.Currency);

                if (currency == null)
                {
                    throw new InvalidOperationException($"Currency '{orderModel.Currency}' not found.");
                }

                return currency.CurrencyId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving currency: {ex.Message}");
                throw;
            }

        }

        private decimal CalculateValue(List<OrderItemModel> items)
        {
            return items.Sum(item => item.Quantity * item.Price);
        }

    }
}
