using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.PaymentRep
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PaymentRepository> _logger;


        public PaymentRepository(AppDbContext context, ILogger<PaymentRepository> logger){
            _context = context;
            _logger = logger;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            try
            {
                await _context.AddAsync(payment);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Pagamento adicionado com sucesso.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar Pagamento no banco de dados.");
                throw new Exception("Erro ao salvar pagamento no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao adicionar o pagamento ao banco de dados;");
                throw new Exception("Ocorreu um erro inesperado ao adicionar o pagamento ao banco de dados;", ex);
            }
        }

        public async Task<bool> DeletePaymentByIdAsync(int idPayment)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(idPayment);
                if (payment == null)
                {
                    _logger.LogWarning($"Pagamento com ID {idPayment} não encontrado.");
                    return false;
                }
                _context.Remove(idPayment);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Pagamento com ID  {idPayment} foi removido com sucesso");
                return true;

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $" Erro ao remover Pagamento com ID  {idPayment} do banco de dados.");
                throw new Exception($"Erro ao remover Pagamento com ID  {idPayment} do banco de dados.", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Erro inesperado ao remover Pagamento com ID  {idPayment} do banco de dados;");
                throw new Exception($"Erro inesperado ao remover Pagamento com ID  {idPayment} do banco de dados;",ex);
            }
        }

        public async Task<IEnumerable<Payment?>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _context.Payments.AsNoTracking().ToListAsync();
                _logger.LogInformation($"Todos os pagamentos foram retornados");
                return payments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de pagamentos.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de pagamentos.", ex);
            }
        }

        public async Task<Payment?> GetPaymentByDateIdAsync(DateTime paymentDate)
        {
            try
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentDate.Date == paymentDate.Date);
                if (payment == null)
                {
                    _logger.LogWarning($"Pagamento com data {paymentDate} não encontrado.");
                }
                return payment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamento por data.");
                throw new Exception("Erro ao buscar pagamento por data.", ex);
            }
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(id);
                if (payment == null)
                {
                    _logger.LogWarning($"Pagamento com ID {id} não encontrado.");
                }
                return payment;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamento por ID.");
                throw new Exception("Erro ao buscar pagamento por ID.", ex);
            }
        }

        public async Task<Payment?> GetPaymentByTenantIdAsync(int tenantId)
        {
            try
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.TenantId == tenantId);
                if (payment == null)
                {
                    _logger.LogWarning($"Pagamento com Tenant ID {tenantId} não encontrado.");
                }
                return payment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamento por Tenant ID.");
                throw new Exception("Erro ao buscar pagamento por Tenant ID.", ex);
            }
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            try
            {
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Pagamento com ID {payment.PaymentId} atualizado com sucesso.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pagamento no banco de dados.");
                throw new Exception("Erro ao atualizar pagamento no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao atualizar o pagamento no banco de dados.");
                throw new Exception("Ocorreu um erro inesperado ao atualizar o pagamento no banco de dados.", ex);
            }
        }
    }
}
