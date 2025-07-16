using Saas.Models;

namespace Saas.Repository.OrderRep
{
    public interface IOrderRepository{
        Task AddOrderAsync(Order order);
        Task<bool>UpdateOrderByIdAsync(int orderId, Order updatedOrder);
        Task<bool>DeleteOrderByIdAsync(int orderId );
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order?>> GetAllOrdersAsync();


    }
}
