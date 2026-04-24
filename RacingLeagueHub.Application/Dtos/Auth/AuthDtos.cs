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
    string Password
);

public record RefreshTokenRequest(
    string RefreshToken
);

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiry,
    UserDto User
);