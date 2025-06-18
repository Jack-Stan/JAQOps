using Microsoft.AspNetCore.Mvc;
using JAQOps.Business.Interfaces;
using JAQOps.API.Models.Auth;

namespace JAQOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.AuthenticateAsync(request.Email, request.Password);
        
        if (!result.Success)
        {
            return Unauthorized(new { message = result.ErrorMessage });
        }
        
        return Ok(new LoginResponse
        {
            Token = result.Token!,
            User = new UserDto
            {
                Id = result.User!.Id,
                Email = result.User.Email,
                FirstName = result.User.FirstName,
                LastName = result.User.LastName,
                PhoneNumber = result.User.PhoneNumber,
                ProfileImageUrl = result.User.ProfileImageUrl,
                IsActive = result.User.IsActive,
                TenantId = result.User.TenantId,
                Roles = result.User.Roles
            }
        });
    }
}
