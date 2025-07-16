using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saas.Services.TenantServices;

namespace Saas.Controllers.LoginController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantLoginController : ControllerBase{
        
        private readonly ILogger<TenantLoginController> _logger;
        private readonly ITenantAuthService _tenantAuthService;

        public TenantLoginController(ILogger<TenantLoginController> logger, ITenantAuthService tenantAuthService){
            _logger = logger;
            _tenantAuthService = tenantAuthService;
        }
    }
}
