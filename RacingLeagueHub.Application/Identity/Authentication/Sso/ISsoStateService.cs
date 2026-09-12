namespace RacingLeagueHub.Application.Identity.Authentication.Sso;

public interface ISsoStateService
{
    string GenerateState();
    void SetStateCookie(string state);
    bool ValidateAndClearState(string? returnedState);
}