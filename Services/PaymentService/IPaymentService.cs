using Saas.Services.DTOs.PaymentDto;

namespace Saas.Services.PaymentService
{
    public interface IPaymentService{
        Task<bool> CreatPaymentAsync(PaymentCreatDto paymentCreatDto);
        Task<bool> DeletePaymentByIdAsync(int id);
        Task<PaymentResponseDto> GetPaymentByIdAsync(int id); 
        Task<IEnumerable<PaymentResponseDto>> GetPaymentAllAsync();
    }
}
