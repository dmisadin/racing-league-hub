namespace RacingLeagueHub.Application.Models.Auth;

public class LoginModel
{
    public bool IsSuccess { get; set; }
    public string Jwt { get; set; }
    public string Message { get; set; }
    public ErrorType Error { get; set; }
}

public enum ErrorType
{
    None = 0,
    InvalidCredentials,
    NotActive
}