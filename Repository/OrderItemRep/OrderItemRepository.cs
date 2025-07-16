using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.OrderItemRep
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderItemRepository> _logger;

        public OrderItemRepository(AppDbContext context, ILogger<OrderItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            try
            {
                await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Item do pedido {orderItem.OrderItemId} adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar item ao pedido.");
                throw;
            }
        }

        public async Task<bool> UpdateOrderItemAsync(OrderItem orderItem)
        {
            try
            {
                var existingOrderItem = await _context.OrderItems.FindAsync(orderItem.OrderItemId);
                if (existingOrderItem == null)
                {
                    _logger.LogWarning($"Item do pedido {orderItem.OrderItemId} não encontrado.");
                    return false;
                }

                existingOrderItem.Quantity = orderItem.Quantity;
                existingOrderItem.PriceAtPurchase = orderItem.PriceAtPurchase;
                existingOrderItem.DiscountPercentage = orderItem.DiscountPercentage;
                existingOrderItem.Status = orderItem.Status;
                existingOrderItem.UpdatedAt = DateTime.UtcNow;

                _context.OrderItems.Update(existingOrderItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Item do pedido {orderItem.OrderItemId} atualizado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar item do pedido {orderItem.OrderItemId}.");
                throw;
            }
        }

        public async Task<bool> DeleteOrderItemByIdAsync(int id)
        {
            try
            {
                var orderItem = await _context.OrderItems.FindAsync(id);
                if (orderItem == null)
                {
                    _logger.LogWarning($"Item do pedido {id} não encontrado.");
                    return false;
                }

                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Item do pedido {id} removido com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover item do pedido {id}.");
                throw;
            }
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
        {
            try
            {
                return await _context.OrderItems
                    .AsNoTracking()
                    .Include(oi => oi.Product)
                    .Include(oi => oi.Order)
                    .FirstOrDefaultAsync(oi => oi.OrderItemId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar item do pedido {id}.");
                throw;
            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            try
            {
                return await _context.OrderItems
                    .Where(oi => oi.OrderId == orderId)
                    .Include(oi => oi.Product)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar itens do pedido {orderId}.");
                throw;
            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByProductIdAsync(int productId)
        {
            try
            {
                return await _context.OrderItems
                    .Where(oi => oi.ProductId == productId)
                    .Include(oi => oi.Order)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar itens do produto {productId}.");
                throw;
            }
        }

        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            try
            {
                var total = await _context.OrderItems
                    .Where(oi => oi.OrderId == orderId)
                    .SumAsync(oi => oi.TotalItemValue);

                _logger.LogInformation($"Total do pedido {orderId} calculado com sucesso.");
                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao calcular o total do pedido {orderId}.");
                throw;
            }
        }

        public async Task<bool> CheckProductAvailabilityAsync(int productId, int quantity)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    _logger.LogWarning($"Produto {productId} não encontrado.");
                    return false;
                }

                return product.QuantityInStock >= quantity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao verificar disponibilidade do produto {productId}.");
                throw;
            }
        }
    }
}
