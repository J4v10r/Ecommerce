using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using AutoMapper;
using Saas.Services.DTOs.PlanDto;
using Saas.Models;
using Saas.Repository.PaymentRep;
using Saas.Services.DTOs.PaymentDto;

namespace Saas.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(IPaymentRepository paymentRepository,IMapper mapper, ILogger<PaymentService> logger){
            _paymentRepository = paymentRepository;
            _mapper = mapper; 
            _logger = logger;
        }

        public async Task<bool> CreatPaymentAsync(PaymentCreatDto paymentCreatDto)
        {
            try {
                var payment = _mapper.Map<Payment>(paymentCreatDto);

                await _paymentRepository.AddPaymentAsync(payment);
                _logger.LogInformation("Pagamento criado com sucesso");
                return true;
            }
            catch (SqlTypeException ex)
            {
                _logger.LogError(ex, "Erro ao acessar o banco de dados ao criar o pagamento.");
                return false;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Erro de validação ao criar o pagamento.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar criar o plano. Detalhes: {paymentCreatDto}", paymentCreatDto);
                return false;
            }
        }

        public async Task<bool> DeletePaymentByIdAsync(int id)
        {
            try
            {
                var result = await _paymentRepository.DeletePaymentByIdAsync(id);
                if (!result)
                {
                    _logger.LogWarning($"Falha ao tentar remover o Pagamento com ID {id}.");
                    return false;
                }
                _logger.LogInformation($"Pagamento com ID {id} removido com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o Pagamento.");
                throw new Exception("Erro ao remover o Pagamento. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetPaymentAllAsync()
        {
            try
            {
                var payments = await _paymentRepository.GetAllPaymentsAsync();
                if (payments == null || !payments.Any())
                {
                    _logger.LogWarning("Lista de Pagamentos não encontrada ou está vazia.");
                    throw new Exception("Lista de Pagamentos não pôde ser retornada.");
                }
                var result = _mapper.Map<IEnumerable<PaymentResponseDto>>(payments);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar o Pagamento.");
                return Enumerable.Empty<PaymentResponseDto>();
            }
        }

        public async Task<PaymentResponseDto> GetPaymentByIdAsync(int id)
        {
            try
            {
                var result = await _paymentRepository.GetPaymentByIdAsync(id);

                if (result == null)
                {
                    _logger.LogWarning($"Erro ao buscar o Pagamento de id {id}. plano não encontrado.");
                    throw new Exception($"Pagamento com ID {id} não encontrado.");
                }
                _logger.LogInformation($"Pagamento de id {id} retornado com sucesso.");
                return _mapper.Map<PaymentResponseDto>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar buscar o Pagamento de id {id}");
                throw new Exception("Ocorreu um erro ao buscar o Pagamento");
            }
        }
    }
}
