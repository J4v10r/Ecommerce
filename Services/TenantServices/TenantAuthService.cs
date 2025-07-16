using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Saas.Models;
using Saas.Services.AuthService;
using Saas.Services.DTOs.TenantDto;

namespace Saas.Services.TenantServices
{
    public class TenantAuthService : ITenantAuthService
    {
        private readonly ILogger<TenantAuthService> _logger;
        private readonly ITenantService _tenantService;
        private readonly IPasswordHasher<Tenant> _passwordHasher;
        private readonly JwtSettings _jwtSettings;

        public TenantAuthService(
            ILogger<TenantAuthService> logger,
            IOptions<JwtSettings> jwtOptions,
            IPasswordHasher<Tenant> passwordHasher,
            ITenantService tenantService)
        {
            _logger = logger;
            _jwtSettings = jwtOptions.Value;
            _tenantService = tenantService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string?> Authenticate(TenantLoginDto tenantLoginDto)
        {
            var tenant = await _tenantService.GetTenantByEmailAsync(tenantLoginDto.Email);
            if (tenant == null)
            {
                _logger.LogWarning("Tenant não encontrado com o email {Email}", tenantLoginDto.Email);
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(tenant, tenant.PasswordHash, tenantLoginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Senha inválida para o tenant {Email}", tenantLoginDto.Email);
                return null;
            }

            return GenerateToken(tenant);
        }

        private string GenerateToken(Tenant tenant)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, tenant.TenantId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, tenant.TenantEmail),
                new Claim("name", tenant.TenantName),
                new Claim("phone", tenant.TenantPhoneNumber),
                new Claim("cpf", tenant.TenantCpf),
                new Claim("planId", tenant.PlanId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

