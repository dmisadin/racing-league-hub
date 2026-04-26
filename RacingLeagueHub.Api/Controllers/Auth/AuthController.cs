using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RacingLeagueHub.Application.Dtos.Auth;
using RacingLeagueHub.Application.Dtos.User;
using RacingLeagueHub.Application.Services.Identity;
using System.Security.Claims;

namespace RacingLeagueHub.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest registerDto, CancellationToken ct)
    {
        var result = await authService.RegisterAsync(registerDto, ct);
        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest loginDto, CancellationToken ct)
    {
        var result = await authService.LoginAsync(loginDto, ct);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(CancellationToken ct)
    {
        var result = await authService.RefreshTokenAsync(ct);
        return Ok(result);
    }

    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> Revoke(CancellationToken ct)
    {
        await authService.RevokeTokenAsync(ct);
        return NoContent();
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var claims = User.Claims.ToDictionary(c => c.Type, c => c.Value);

        return Ok(new UserDto(
            id: long.Parse(claims[JwtRegisteredClaimNames.Sub]),
            email: claims[JwtRegisteredClaimNames.Email],
            username: claims["username"],
            isAdmin: claims[ClaimTypes.Role] == "Admin",
            driverId: claims.TryGetValue("driverId", out var driverId)
                ? long.Parse(driverId)
                : null
        ));
    }
    /*
    [HttpPost("forgot-password")]
    public LoginModel ForgotPassword([FromBody] LoginDto loginDto)
    {
    }
    */
}
