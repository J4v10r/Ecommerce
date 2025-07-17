using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Saas_Sexshop.Dto.TenantDto;
using Saas_Sexshop.Models;
using Saas_Sexshop.Repository.TenantRep;
using Saas_Sexshop.Services.TenantServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Saas_Sexshop.Services.AuthService
{
    public class AuthService: IAuthService
    {
        private readonly ITenantService _tenantService;
        private readonly IPasswordHasher<Tenant> _passwordHasher;
        private readonly JwtSettings _jwtSettings;
        public AuthService(ITenantService tenantService, IPasswordHasher<Tenant> passwordHasher, IOptions<JwtSettings> jwtSettings)
        {
            _tenantService = tenantService;
            _passwordHasher = passwordHasher;
            _jwtSettings = jwtSettings.Value;

        }

        async Task<string?> IAuthService.AuthTenant(string email, string password)
        {
            var tenant = await _tenantService.GetTenantByEmailAsync(email);
            if (tenant == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(tenant, tenant.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var token = GenerateToken(tenant);
            return token;
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
