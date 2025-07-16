using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Saas.Models;
using Saas.Services.AuthService;
using Saas.Services.DTOs.AdminDto;

namespace Saas.Services.AdminService
{
    public class AdminAuthService : IAdminAuthService
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminAuthService> _logger;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public AdminAuthService(
            IAdminService adminService,
            ILogger<AdminAuthService> logger,
            IOptions<JwtSettings> jwtOptions,
            IPasswordHasher<Admin> passwordHasher)
        {
            _adminService = adminService;
            _logger = logger;
             _jwtSettings = jwtOptions.Value;
            _passwordHasher = passwordHasher;
        }

        async Task<string> IAdminAuthService.AuthenticateAsync(AdminLoginDto adminLoginDto)
        {

            var admin = await _adminService.GetAdminByNameAsync(adminLoginDto.Name);
            if (admin == null)
            {
                _logger.LogWarning("Administrador não encontrado com o nome {Name}", adminLoginDto.Name);
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(admin, admin.PasswordHash, adminLoginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Senha inválida para o administrador {Name}", adminLoginDto.Name);
                return null;
            }

            return GenerateToken(admin);
        }
        private string GenerateToken(Admin admin)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.AdminId.ToString()),
                new Claim("name", admin.Name),
                new Claim("role", "admin"),
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

