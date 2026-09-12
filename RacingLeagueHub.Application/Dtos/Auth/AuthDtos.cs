using RacingLeagueHub.Application.Users.Dtos;

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