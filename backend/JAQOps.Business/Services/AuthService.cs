using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JAQOps.Business.Interfaces;
using JAQOps.Business.Models;
using JAQOps.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace JAQOps.Business.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<AuthResult> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Invalid email or password"
            };
        }

        if (!user.IsActive)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "User account is deactivated"
            };
        }

        // Verify password
        if (!BC.Verify(password, user.PasswordHash))
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Invalid email or password"
            };
        }

        // Get user roles
        var roles = await _userRepository.GetUserRolesAsync(user.Id);

        // Generate token
        var token = await GenerateJwtTokenAsync(user.Id, user.Email, user.TenantId, roles);

        // Update last login
        user.LastLoginDate = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return new AuthResult
        {
            Success = true,
            Token = token,
            User = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfileImageUrl,
                IsActive = user.IsActive,
                TenantId = user.TenantId,
                Roles = roles.ToList()
            }
        };
    }    public Task<string> GenerateJwtTokenAsync(Guid userId, string email, Guid? tenantId, IEnumerable<string> roles)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["JwtSettings:SecretKey"] ?? "DefaultSecretKeyForDevelopment"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add tenant claim if applicable
        if (tenantId.HasValue)
        {
            claims.Add(new Claim("tenant_id", tenantId.Value.ToString()));
        }

        // Add role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"] ?? "60")),
            signingCredentials: credentials
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
