using AbySalto.Junior.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<OrderModel>> GetOrders();
        Task<int> AddOrderAsync(OrderModel order);
        Task<bool> ChangeOrderStatus(int orderId, int statusId);
        Task<IEnumerable<OrderModel>> SortOrdersByValue();
        Task<decimal> GetTotalOrdersValue(int userId);
    }
}
