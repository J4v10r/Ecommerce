using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Saas.Models;
using Saas.Services.AuthService;
using Saas.Services.DTOs.UserDto;

namespace Saas.Services.UserServices
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserAuthService> _logger;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserAuthService(
            IUserService userService,
            ILogger<UserAuthService> logger,
            IOptions<JwtSettings> jwtOptions,
            IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _logger = logger;
            _jwtSettings = jwtOptions.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<string?> Authenticate(UserLoginDto userLoginDto)
        {
            var user = await _userService.GetUserByEmailAsync(userLoginDto.Email);
            if (user == null)
            {
                _logger.LogWarning("Usuário não encontrado com o email {Email}", userLoginDto.Email);
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Senha inválida para o usuário {Email}", userLoginDto.Email);
                return null;
            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
                new Claim("name", user.UserName),
                new Claim("phone", user.UserPhone),
                new Claim("cpf", user.UserCpf),
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




