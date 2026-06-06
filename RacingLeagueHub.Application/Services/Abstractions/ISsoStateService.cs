namespace RacingLeagueHub.Application.Services.Abstractions;

public interface ISsoStateService
{
    string GenerateState();
    void SetStateCookie(string state);
    bool ValidateAndClearState(string? returnedState);
}