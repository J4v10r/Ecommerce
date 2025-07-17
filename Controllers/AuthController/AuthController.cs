using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saas_Sexshop.Dto.TenantDto;
using Saas_Sexshop.Services.AuthService;

namespace Saas_Sexshop.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TenantLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _authService.AuthTenant(loginDto.Email, loginDto.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Credenciais inválidas.");
            }
            return Ok(new { Token = token });
        }
    }
}
