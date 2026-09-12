using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacingLeagueHub.Application.Identity.Authentication.Dtos;
using RacingLeagueHub.Application.Identity.Authentication.ExternalLogins.Persistence;
using RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;
using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes;
using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;
using RacingLeagueHub.Application.Identity.Authentication.RefreshTokens.Persistence;
using RacingLeagueHub.Application.Identity.Authentication.Sso.Models;
using RacingLeagueHub.Application.Users.Dtos;
using RacingLeagueHub.Application.Users.Persistence;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Utilities;
using RacingLeagueHub.Identity.Authentication.Persistence;
using System.Security.Cryptography;

namespace RacingLeagueHub.Application.Identity.Authentication;

public class AuthService(
    IUserQueries userQueries,
    IUserCommands userCommands,
    IRefreshTokenQueries refreshTokenQueries,
    IRefreshTokenCommands refreshTokenCommands,
    IPasswordResetTokenQueries passwordResetTokenQueries,
    IPasswordResetTokenCommands passwordResetTokenCommands,
    IUserRecoveryCodeQueries userRecoveryCodeQueries,
    IUserExternalLoginQueries externalLoginQueries,
    IUserExternalLoginCommands externalLoginCommands,
    IJwtService jwtService,
    ITotpService totpService,
    IRecoveryCodeService recoveryCodeService,
    IPasswordHasher<User> passwordHasher,
    IHttpContextAccessor httpContextAccessor
) : IAuthService
{

    public async Task<AuthResponse> RegisterAsync(RegisterRequest req, CancellationToken ct = default)
    {
        if (await userQueries.IsEmailTakenAsync(req.Email, ct))
            throw new InvalidOperationException("Email already in use.");

        if (await userQueries.IsUsernameTakenAsync(req.Username, ct))
            throw new InvalidOperationException("Username already in use.");

        var user = new User
        {
            Username = req.Username,
            Email = req.Email,
            IsAdmin = false,
            CreatedAt = DateTime.UtcNow,
        };

        user.PasswordHash = passwordHasher.HashPassword(user, req.Password);

        await userCommands.AddAsync(user, ct);

        return await BuildAuthResponse(user, rememberMe: false, ct);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest req, CancellationToken ct = default)
    {
        var user = await userQueries.GetUserAsync(req.Email, ct)
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

        var user = await userQueries.GetUserAsync(userId, ct)
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

        await userCommands.SaveChangesAsync(ct);

        return await BuildAuthResponse(user, req.RememberMe, ct);
    }

    public async Task<AuthResponse> RefreshTokenAsync(CancellationToken ct = default)
    {
        var refreshToken = httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"]
                           ?? throw new UnauthorizedAccessException("Refresh token not found.");

        var token = await refreshTokenQueries.GetRefreshTokenWithUserAsync(refreshToken, ct)
            ?? throw new UnauthorizedAccessException("Invalid refresh token.");

        if (!token.IsActive)
            throw new UnauthorizedAccessException("Refresh token expired or revoked.");

        token.IsRevoked = true;
        await refreshTokenCommands.SaveChangesAsync(ct);

        return await BuildAuthResponse(token.User, rememberMe: IsLongLivedCookie(), ct);
    }

    public async Task RevokeTokenAsync(CancellationToken ct = default)
    {
        var refreshToken = httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"]
                           ?? throw new UnauthorizedAccessException("Token not found.");

        await refreshTokenCommands.RevokeRefreshTokenAsync(refreshToken, ct);

        ClearRefreshTokenCookie();
    }

    public async Task<AuthResponse> LoginWithGoogleAsync(
        GoogleUserInfo googleUser,
        CancellationToken ct = default)
    {
        if (!googleUser.EmailVerified)
            throw new UnauthorizedAccessException("Google email is not verified.");

        const string provider = "Google";

        var externalLogin = await externalLoginQueries.FindByProviderAsync(
            provider,
            googleUser.ProviderUserId,
            ct);

        if (externalLogin is not null)
        {
            return await BuildAuthResponse(
                externalLogin.User,
                rememberMe: true,
                ct);
        }

        var user = await userQueries.GetUserAsync(googleUser.Email, ct);

        if (user is null)
        {
            user = new User
            {
                Email = googleUser.Email,
                Username = await GenerateUniqueUsernameAsync(googleUser.Email, ct),
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow,

                // Important:
                // external-login-only users may not have a password.
                PasswordHash = string.Empty
            };

            await userCommands.AddAsync(user, ct);
        }

        await externalLoginCommands.AddAsync(new UserExternalLogin
        {
            UserId = user.Id,
            Provider = provider,
            ProviderUserId = googleUser.ProviderUserId,
            Email = googleUser.Email,
            DisplayName = googleUser.Name,
            PictureUrl = googleUser.PictureUrl,
            CreatedAt = DateTime.UtcNow
        }, ct);

        return await BuildAuthResponse(user, rememberMe: true, ct);
    }

    private async Task UseRecoveryCodeAsync(long userId, string code, CancellationToken ct)
    {
        var unusedCodes = await userRecoveryCodeQueries.GetUnusedForUserAsync(userId, ct);

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

        await refreshTokenCommands.AddAsync(rawRefresh, user.Id, expiry);

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
        var user = await userQueries.GetUserAsync(req.Email, ct);
        if (user is null) return;

        await passwordResetTokenCommands.InvalidateUserTokensAsync(user.Id, ct);

        var rawToken = GenerateResetToken();

        await passwordResetTokenCommands.AddAsync(rawToken, user.Id);
        
        Console.WriteLine($"http://localhost:4200/auth/reset-password?token={rawToken}");
        
        // TODO: send email with reset link
        // e.g. https://localhost:4200/auth/reset-password?token={rawToken}
        // await emailService.SendPasswordResetAsync(user.Email, rawToken);
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest req, CancellationToken ct = default)
    {
        if (req.NewPassword != req.ConfirmPassword)
            throw new InvalidOperationException("Passwords do not match.");

        var token = await passwordResetTokenQueries.GetTokenWithUserAsync(req.Token, ct)
                    ?? throw new InvalidOperationException("Invalid or expired reset token.");

        if (!token.IsActive)
            throw new InvalidOperationException("Invalid or expired reset token.");

        token.User.PasswordHash = passwordHasher.HashPassword(token.User, req.NewPassword);
        token.IsUsed = true;

        await passwordResetTokenCommands.SaveChangesAsync(ct);
    }

    private static string GenerateResetToken()
    {
        var bytes = new byte[64];
        RandomNumberGenerator.Fill(bytes);
        return Base64UrlUtility.Encode(bytes);
    }

    private async Task<string> GenerateUniqueUsernameAsync(string email, CancellationToken ct)
    {
        var baseUsername = email.Split('@')[0]
            .ToLowerInvariant()
            .Where(char.IsLetterOrDigit)
            .Aggregate("", (current, c) => current + c);

        if (string.IsNullOrWhiteSpace(baseUsername))
            baseUsername = "user";

        var username = baseUsername;
        var counter = 1;

        while (await userQueries.IsUsernameTakenAsync(username, ct))
        {
            username = $"{baseUsername}{counter}";
            counter++;
        }

        return username;
    }
}