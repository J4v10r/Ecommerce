using Saas.Models;

namespace Saas.Repository.PaymentRep
{
    public interface IPaymentRepository{
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentByIdAsync(int  idPayment);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment?>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByTenantIdAsync(int tenantId);
        Task<Payment?> GetPaymentByDateIdAsync(DateTime paymentDate);

    }
}
