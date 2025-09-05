using AbySalto.Junior.Application.DTO;

namespace AbySalto.Junior.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<OrderModel>> GetOrders();
    }
}
