using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.OrderRep
{
    public class OrderRepository : IOrderRepository {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(AppDbContext context, ILogger<OrderRepository> logger ){
            _context = context;
            _logger = logger;
        }

        public async Task AddOrderAsync(Order order)
        {
            try{
                await _context.AddAsync(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Pedido {order.OrderId} adicionado com sucesso.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar pedido no banco de dados.");
                throw new Exception("Erro ao salvar pedido no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao adicionar o pedido ao banco de dados;");
                throw new Exception("Ocorreu um erro inesperado ao adicionar o pedido ao banco de dados;", ex);
            }
        }

        public async Task<bool>DeleteOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogWarning($"Pedido com ID {orderId} não encontrado.");
                    return false;
                }
                _context.Remove(orderId);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Pedido com ID {orderId} foi removido com sucesso");
                return true;

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao remover pedido com ID {orderId} no banco de dados.");
                throw new Exception($"Erro ao remover o pedido com ID {orderId} no banco de dados.", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao remover o pedido com ID {orderId} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao remover o pedido com ID {orderId} ao banco de dados;", ex);
            }
        }
    
        public async Task<IEnumerable<Order?>> GetAllOrdersAsync()
        {
            try
            {
                var order = await _context.Orders.AsNoTracking().ToListAsync();
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de pedidos.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de pedidos.", ex);
            }
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(p => p.OrderId == orderId);
                if (order == null)
                {
                    _logger.LogWarning($"O pedido com id {orderId} não foi encontrado.");
                }
                return order;
            }
            catch (Exception ex){
                _logger.LogError(ex, $"Erro inesperado tentar obter o pedido de id {orderId}");
                throw new Exception($"Erro inesperado tentar obter o pedido de id {orderId}", ex);

            }
        }

        public async Task<bool>UpdateOrderByIdAsync(int orderId, Order updatedOrder){
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogWarning($"O pedido com ID {orderId} não foi encontrado.");
                    return false;
                }

                order.OrderDate = updatedOrder.OrderDate;
                order.TotalAmount = updatedOrder.TotalAmount;
                order.Status = updatedOrder.Status;
                order.UserId = updatedOrder.UserId;
                order.TenantId = updatedOrder.TenantId;
                order.OrderPaymentId = updatedOrder.OrderPaymentId;
                order.OrderItems = updatedOrder.OrderItems;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Pedido com ID {orderId} atualizado com sucesso.");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o pedido com ID {orderId}.");
                throw new Exception($"Erro ao atualizar o pedido com ID {orderId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao atualizar o pedido com ID {orderId}.");
                throw new Exception($"Erro inesperado ao atualizar o pedido com ID {orderId}.", ex);
            }
        }
    }
}
