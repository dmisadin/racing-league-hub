using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacingLeagueHub.Application.Dtos.Auth;
using RacingLeagueHub.Application.Dtos.User;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Services.Identity;

public class AuthService(
    IUserRepository userRepository,
    IRefreshTokenRepository tokenRepository,
    IJwtService jwtService,
    IPasswordHasher<User> passwordHasher,
    IHttpContextAccessor httpContextAccessor
) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest req, CancellationToken ct = default)
    {
        if (await userRepository.IsEmailTakenAsync(req.Email, ct))
            throw new InvalidOperationException("Email already in use.");

        if (await userRepository.IsUsernameTakenAsync(req.Username, ct))
            throw new InvalidOperationException("Username already in use.");

        var user = new User
        {
            Username = req.Username,
            Email = req.Email,
            IsAdmin = false,
            CreatedAt = DateTime.UtcNow,
        };

        user.PasswordHash = passwordHasher.HashPassword(user, req.Password);

        await userRepository.InsertAsync(user);
        await userRepository.CommitAsync(ct);

        return await BuildAuthResponse(user, rememberMe: false, ct);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest req, CancellationToken ct = default)
    {
        var user = await userRepository.FindByEmailAsync(req.Email, ct)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid credentials.");

        return await BuildAuthResponse(user, req.RememberMe, ct);
    }

    public async Task<AuthResponse> RefreshTokenAsync(CancellationToken ct = default)
    {
        var refreshToken = httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"]
                           ?? throw new UnauthorizedAccessException("Refresh token not found.");

        var token = await tokenRepository.GetRefreshTokenWithUserAsync(refreshToken, ct)
            ?? throw new UnauthorizedAccessException("Invalid refresh token.");

        if (!token.IsActive)
            throw new UnauthorizedAccessException("Refresh token expired or revoked.");

        token.IsRevoked = true;
        await tokenRepository.CommitAsync(ct);

        return await BuildAuthResponse(token.User, rememberMe: IsLongLivedCookie(), ct);
    }

    public async Task RevokeTokenAsync(CancellationToken ct = default)
    {
        var refreshToken = httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"]
                           ?? throw new UnauthorizedAccessException("Token not found.");

        var token = await tokenRepository.GetRefreshTokenAsync(refreshToken, ct)
            ?? throw new UnauthorizedAccessException("Token not found.");

        token.IsRevoked = true;
        await tokenRepository.CommitAsync(ct);

        ClearRefreshTokenCookie();
    }
    
    private async Task<AuthResponse> BuildAuthResponse(User user, bool rememberMe, CancellationToken ct)
    {
        var accessToken = jwtService.GenerateAccessToken(user);
        var rawRefresh = jwtService.GenerateRefreshToken();
        var expiry = rememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(8);

        await tokenRepository.InsertAsync(new RefreshToken
        {
            Token = rawRefresh,
            UserId = user.Id,
            ExpiresAt = expiry,
        });

        await tokenRepository.CommitAsync(ct);

        SetRefreshTokenCookie(rawRefresh, rememberMe ? expiry : null);

        return new AuthResponse(
            AccessToken: accessToken,
            AccessTokenExpiry: jwtService.GetAccessTokenExpiry(),
            User: new UserDto(user.Id, user.Email, user.Username, user.IsAdmin, user.DriverId)
        );
    }

    private void SetRefreshTokenCookie(string token, DateTime? expires)
    {
        httpContextAccessor.HttpContext!.Response.Cookies.Append("refresh_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = expires,
            Path = "/api/auth"
        });

        if (expires.HasValue)
        {
            httpContextAccessor.HttpContext!.Response.Cookies.Append("remember_me", "true", new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expires,
                Path = "/api/auth"
            });
        }
    }

    private void ClearRefreshTokenCookie()
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/api/auth"
        };

        httpContextAccessor.HttpContext!.Response.Cookies.Delete("refresh_token", options);
        httpContextAccessor.HttpContext!.Response.Cookies.Delete("remember_me", new CookieOptions
        {
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/api/auth"
        });
    }

    private bool IsLongLivedCookie()
    {
        var request = httpContextAccessor.HttpContext?.Request;
        return request?.Cookies.ContainsKey("remember_me") == true;
    }
}