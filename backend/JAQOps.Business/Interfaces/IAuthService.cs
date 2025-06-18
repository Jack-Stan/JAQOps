using JAQOps.Business.Models;

namespace JAQOps.Business.Interfaces;

public interface IAuthService
{
    Task<AuthResult> AuthenticateAsync(string email, string password);
    Task<string> GenerateJwtTokenAsync(Guid userId, string email, Guid? tenantId, IEnumerable<string> roles);
}

public class AuthResult
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public UserModel? User { get; set; }
    public string? ErrorMessage { get; set; }
}
