using Saas.Services.DTOs.OrderDto;

namespace Saas.Services.OrderService
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(OrderCreateDto order);
        Task<bool> DeleteOrderByIdAsync(int orderId);
        Task<OrderResponseDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();

    }
}
