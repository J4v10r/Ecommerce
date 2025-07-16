using Saas.Models;
using Saas.Services.DTOs.OrderItemDto;

namespace Saas.Services.OrderItemService
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync(OrderItemCreateDto dto);
        Task<bool> UpdateOrderItemAsync(OrderItemCreateDto dto);
        Task<bool> DeleteOrderItemByIdAsync(int id);
        Task<OrderItemResponseDto> GetOrderItemByIdAsync(int id);
        Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsByOrderIdAsync(int id);
        Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsByProductIdAsync(int id);
        Task<decimal> CalculateOrderTotalAsync(int id);
        Task<bool> CheckProductAvailabilityAsync(int id, int quantity);

    }
}
