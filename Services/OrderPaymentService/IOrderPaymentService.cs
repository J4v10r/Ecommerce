using Saas.Models;

namespace Saas.Services.OrderPaymentService
{
    public interface IOrderPaymentService
    {
        Task AddOrderPayment(OrderPayment orderPayment);
        Task UpdateOrderPaymentAsync(int id);
        Task<bool> DeleteOrderPaymentByIdAsync(int id);
        Task<OrderPayment?> GetOrderPaymentByIdAsync(int id);
        Task<IEnumerable<OrderPayment?>> GetAllOrderPaymentsAsync();
        Task<OrderPayment?> GeOrdeertPaymentByDateIdAsync(DateTime paymentDate);
    }
}
