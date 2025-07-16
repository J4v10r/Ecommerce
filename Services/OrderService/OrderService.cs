using AutoMapper;
using Microsoft.Extensions.Logging;
using Saas.Services.DTOs.TenantDto;
using Saas.Repository.OrderRep;
using Saas.Services.DTOs.OrderDto;

namespace Saas.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderService> logger){
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
            
        }


        public async Task<bool> AddOrderAsync(OrderCreateDto orderDto)
        {
            if (orderDto == null)
            {
                _logger.LogWarning("AddOrderAsync recebeu um objeto 'order' nulo.");
                return false;

            }
            try
            {
                var ordMap = _mapper.Map<Order>(orderDto);
                await _orderRepository.AddOrderAsync(ordMap);
                return true;
            }
            catch (Exception ex) {
                _logger.LogError($"Erro inesperado ao adicionar Order.{ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteOrderByIdAsync(int orderId)
        {
            if (orderId <= 0)
            {
                _logger.LogWarning("Erro: Tentativa de excluir pedido com ID inválido: {OrderId}", orderId);
                throw new ArgumentOutOfRangeException(nameof(orderId), "O ID do pedido deve ser um valor positivo.");
            }
            try
            {
                await _orderRepository.DeleteOrderByIdAsync(orderId);
                _logger.LogInformation("O pedido com ID {OrderId} foi deletado com sucesso.", orderId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar excluir o pedido com ID {OrderId}.", orderId); 
                throw;
            }
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();
                if (orders == null || !orders.Any())
                {
                    _logger.LogWarning("A lista de pedidos está vazia");
                    throw new Exception("Lista de pedidos nao pode ser retornada.");
                }
                var result = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
                return result;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro inesperado ao buscar o Vendedores.");
                return Enumerable.Empty<OrderResponseDto>();

            }
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int orderId)
        {
            if (orderId <= 0)
            {
                _logger.LogWarning("Erro: O id foi menor ou igual a 0");
                _logger.LogWarning($"Erro: Pedido de id {orderId} não encontrado.");
                throw new Exception($"pedido com ID {orderId} não encontrado.");
            }
            try
            {
                var result = await _orderRepository.GetOrderByIdAsync(orderId);
                return _mapper.Map<OrderResponseDto>(result);

                

            }
            catch (Exception ex){
                _logger.LogError($"Erro inesperado: Erro ao tentar buscar o pedido de id {orderId}",ex);
                throw;
            }
        }
    }
}
