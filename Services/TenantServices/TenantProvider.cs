namespace Saas.Services.TenantServices
{
    public class TenantProvider : ITenantProvider {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetTenantId()
        {
            if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            var tenantIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("TenantId")?.Value;

            if (string.IsNullOrEmpty(tenantIdClaim) || !int.TryParse(tenantIdClaim, out var tenantId))
                throw new InvalidOperationException("TenantId inválido ou não encontrado.");

            return tenantId;
        }
    }
}
