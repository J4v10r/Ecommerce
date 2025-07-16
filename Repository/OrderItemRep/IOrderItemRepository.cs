using Saas.Models;

namespace Saas.Repository.OrderItemRep
{
    public interface IOrderItemRepository{
        Task AddOrderItemAsync(OrderItem orderItem);
        Task<bool>UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool>DeleteOrderItemByIdAsync(int id);
        Task<OrderItem> GetOrderItemByIdAsync(int id);

        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByProductIdAsync(int productId);
        Task<decimal> CalculateOrderTotalAsync(int orderId);

        Task<bool> CheckProductAvailabilityAsync(int productId, int quantity);

    }
}
