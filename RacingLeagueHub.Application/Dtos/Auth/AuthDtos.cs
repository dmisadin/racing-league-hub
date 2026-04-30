using RacingLeagueHub.Application.Dtos.User;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Dtos.Auth;

public record RegisterRequest(
    string Username,
    string Email,
    string Password
);

public record LoginRequest(
    string Email,
    string Password,
    bool RememberMe
);

public record RefreshTokenRequest(
    string RefreshToken
);

public record AuthResponse(
    string AccessToken,
    DateTime AccessTokenExpiry,
    UserDto User
);

public record ForgotPasswordRequest(string Email);

public record ResetPasswordRequest(
    string Token,
    string NewPassword,
    string ConfirmPassword
);