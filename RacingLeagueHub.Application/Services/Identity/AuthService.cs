using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacingLeagueHub.Application.Dtos.Auth;
using RacingLeagueHub.Application.Dtos.User;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Abstractions.Repositories;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Entities.Authentication;
using RacingLeagueHub.Domain.Utilities;
using System.Security.Cryptography;

namespace RacingLeagueHub.Application.Services.Identity;

public class AuthService(
    IUserRepository userRepository,
    IRefreshTokenRepository tokenRepository,
    IPasswordResetTokenRepository passwordResetTokenRepository,
    IUserRecoveryCodeRepository userRecoveryCodeRepository,
    IJwtService jwtService,
    ITotpService totpService,
    IRecoveryCodeService recoveryCodeService,
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

    public async Task<LoginResponse> LoginAsync(LoginRequest req, CancellationToken ct = default)
    {
        var user = await userRepository.FindByEmailAsync(req.Email, ct)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            req.Password
        );

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid credentials.");

        if (user.TwoFactorEnabled)
        {
            var twoFactorToken = jwtService.GenerateTwoFactorToken(user);

            return new LoginResponse(
                RequiresTwoFactor: true,
                Auth: null,
                TwoFactorToken: twoFactorToken
            );
        }

        var authResponse = await BuildAuthResponse(user, req.RememberMe, ct);

        return new LoginResponse(
            RequiresTwoFactor: false,
            Auth: authResponse,
            TwoFactorToken: null
        );
    }

    public async Task<AuthResponse> LoginWithTwoFactorAsync(TwoFactorLoginRequest req, CancellationToken ct = default)
    {
        var principal = jwtService.ValidateTwoFactorToken(req.TwoFactorToken)
            ?? throw new UnauthorizedAccessException("Invalid two-factor token.");

        var userId = jwtService.GetUserIdFromPrincipal(principal);

        var user = await userRepository.GetByIdAsync(userId, user => user,ct)
            ?? throw new UnauthorizedAccessException("Invalid two-factor token.");

        if (!user.TwoFactorEnabled || string.IsNullOrWhiteSpace(user.TwoFactorSecret))
            throw new UnauthorizedAccessException("Two-factor authentication is not enabled.");

        if (req.IsRecoveryCode)
        {
            await UseRecoveryCodeAsync(user.Id, req.Code, ct);
        }
        else
        {
            var valid = totpService.VerifyCode(user.TwoFactorSecret, req.Code, user.LastTotpTimeStepUsed, out var matchedStep);

            if (!valid)
                throw new UnauthorizedAccessException("Invalid authentication code.");

            user.LastTotpTimeStepUsed = matchedStep;
        }

        await userRepository.CommitAsync(ct);

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

    private async Task UseRecoveryCodeAsync(long userId, string code, CancellationToken ct)
    {
        var unusedCodes = await userRecoveryCodeRepository.GetUnusedForUserAsync(userId, ct);

        var matchingCode = unusedCodes.FirstOrDefault(x =>
            recoveryCodeService.VerifyCode(code, x.CodeHash));

        if (matchingCode is null)
            throw new UnauthorizedAccessException("Invalid recovery code.");

        matchingCode.UsedAt = DateTime.UtcNow;
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
    
    public async Task ForgotPasswordAsync(ForgotPasswordRequest req, CancellationToken ct = default)
    {
        var user = await userRepository.FindByEmailAsync(req.Email, ct);
        if (user is null) return;

        await passwordResetTokenRepository.InvalidateUserTokensAsync(user.Id, ct);

        var rawToken = GenerateResetToken();

        await passwordResetTokenRepository.InsertAsync(new PasswordResetToken
        {
            Token = rawToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
        });

        await passwordResetTokenRepository.CommitAsync(ct);
        
        Console.WriteLine($"http://localhost:4200/auth/reset-password?token={rawToken}");
        
        // TODO: send email with reset link
        // e.g. https://localhost:4200/auth/reset-password?token={rawToken}
        // await emailService.SendPasswordResetAsync(user.Email, rawToken);
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest req, CancellationToken ct = default)
    {
        if (req.NewPassword != req.ConfirmPassword)
            throw new InvalidOperationException("Passwords do not match.");

        var token = await passwordResetTokenRepository.GetTokenWithUserAsync(req.Token, ct)
                    ?? throw new InvalidOperationException("Invalid or expired reset token.");

        if (!token.IsActive)
            throw new InvalidOperationException("Invalid or expired reset token.");

        token.User.PasswordHash = passwordHasher.HashPassword(token.User, req.NewPassword);
        token.IsUsed = true;

        await passwordResetTokenRepository.CommitAsync(ct);
    }

    private static string GenerateResetToken()
    {
        var bytes = new byte[64];
        RandomNumberGenerator.Fill(bytes);
        return Base64UrlUtility.Encode(bytes);
    }
}