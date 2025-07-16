using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.OrderPayments
{
    public class OrderPaymentRepository : IOrderPaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderPaymentRepository> _logger;


        public OrderPaymentRepository(AppDbContext appDbContext, ILogger<OrderPaymentRepository> logger){
            _context = appDbContext;
            _logger = logger;
        }

        public Task AddOrderPayment(OrderPayment orderPayment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePaymentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderPayment?>> GetAllPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderPayment?> GetPaymentByDateIdAsync(DateTime paymentDate)
        {
            throw new NotImplementedException();
        }

        public Task<OrderPayment?> GetPaymentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderPayment?> GetPaymentByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePaymentAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
