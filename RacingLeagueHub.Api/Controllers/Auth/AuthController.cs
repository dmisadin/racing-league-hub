using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RacingLeagueHub.Application.Dtos.Auth;
using RacingLeagueHub.Application.Dtos.User;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.Identity;
using RacingLeagueHub.Domain.Abstractions;

namespace RacingLeagueHub.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService,
    ILeagueUserRepository leagueUserRepository) : BaseController
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
    
    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken ct)
    {
        await authService.ForgotPasswordAsync(request, ct);
        return Ok(new { message = "If that email exists, a reset link has been sent." });
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken ct)
    {
        await authService.ResetPasswordAsync(request, ct);
        return Ok(new { message = "Password reset successfully." });
    }
}
