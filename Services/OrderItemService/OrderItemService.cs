using AutoMapper;
using Saas.Models;
using Saas.Repository.OrderItemRep;
using Saas.Repository.OrderRep;
using Saas.Services.DTOs.OrderItemDto;

namespace Saas.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ILogger<OrderItemService> _logger;
        private readonly IMapper _mapper;
        public OrderItemService(IOrderItemRepository orderItemRepository, ILogger<OrderItemService> logger, IMapper mapper){
            _orderItemRepository = orderItemRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AddOrderItemAsync(OrderItemCreateDto dto)
        {
            try
            {
                var orderItem = _mapper.Map<OrderItem>(dto);
                orderItem.CreatedAt = DateTime.UtcNow;
                orderItem.UpdatedAt = DateTime.UtcNow;

                await _orderItemRepository.AddOrderItemAsync(orderItem);
                _logger.LogInformation("Item do pedido adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar item do pedido.");
                throw new ApplicationException("Erro ao adicionar item do pedido.", ex);
            }
        }

        public async Task<bool> UpdateOrderItemAsync(OrderItemCreateDto dto)
        {
            try
            {
                var orderItem = _mapper.Map<OrderItem>(dto);
                orderItem.UpdatedAt = DateTime.UtcNow;

                var result = await _orderItemRepository.UpdateOrderItemAsync(orderItem);
                _logger.LogInformation("Item do pedido atualizado com sucesso.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar item do pedido.");
                throw new ApplicationException("Erro ao atualizar item do pedido.", ex);
            }
        }

        public async Task<bool> DeleteOrderItemByIdAsync(int id)
        {
            try
            {
                var result = await _orderItemRepository.DeleteOrderItemByIdAsync(id);
                if (result)
                {
                    _logger.LogInformation("Item do pedido com ID {OrderItemId} removido com sucesso.", id);
                }
                else
                {
                    _logger.LogWarning("Item do pedido com ID {OrderItemId} não encontrado para remoção.", id);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover item do pedido com ID {OrderItemId}.", id);
                throw new ApplicationException("Erro ao remover item do pedido.", ex);
            }
        }
        public async Task<OrderItemResponseDto> GetOrderItemByIdAsync(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);

                if (orderItem == null)
                {
                    _logger.LogWarning("Item do pedido com ID {OrderItemId} não encontrado.", id);
                    throw new KeyNotFoundException($"Item do pedido com ID {id} não encontrado.");
                }

                _logger.LogInformation("Item do pedido com ID {OrderItemId} recuperado com sucesso.", id);
                return _mapper.Map<OrderItemResponseDto>(orderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar item do pedido com ID {OrderItemId}.", id);
                throw new ApplicationException("Erro ao buscar item do pedido.", ex);
            }
        }

        public async Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsByOrderIdAsync(int id)
        {
            try
            {
                var items = await _orderItemRepository.GetOrderItemsByOrderIdAsync(id);
                _logger.LogInformation("Itens do pedido com OrderId {OrderId} recuperados com sucesso.", id);
                return _mapper.Map<IEnumerable<OrderItemResponseDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar itens do pedido com OrderId {OrderId}.", id);
                throw new ApplicationException("Erro ao buscar itens do pedido.", ex);
            }
        }

        public async Task<IEnumerable<OrderItemResponseDto>> GetOrderItemsByProductIdAsync(int id)
        {
            try
            {
                var items = await _orderItemRepository.GetOrderItemsByProductIdAsync(id);
                _logger.LogInformation("Itens do produto com ProductId {ProductId} recuperados com sucesso.", id);
                return _mapper.Map<IEnumerable<OrderItemResponseDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar itens por produto com ProductId {ProductId}.", id);
                throw new ApplicationException("Erro ao buscar itens por produto.", ex);
            }
        }

        public async Task<decimal> CalculateOrderTotalAsync(int id)
        {
            try
            {
                var total = await _orderItemRepository.CalculateOrderTotalAsync(id);
                _logger.LogInformation("Total do pedido {OrderId} calculado com sucesso.", id);
                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao calcular total do pedido {OrderId}.", id);
                throw new ApplicationException("Erro ao calcular total do pedido.", ex);
            }
        }

        public async Task<bool> CheckProductAvailabilityAsync(int id, int quantity)
        {
            try
            {
                var available = await _orderItemRepository.CheckProductAvailabilityAsync(id, quantity);
                _logger.LogInformation("Disponibilidade do produto {ProductId} verificada: {Available}.", id, available);
                return available;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar disponibilidade do produto {ProductId}.", id);
                throw new ApplicationException("Erro ao verificar disponibilidade do produto.", ex);
            }
        }
    }
}

