namespace RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes;

public interface IRecoveryCodeService
{
    IReadOnlyList<string> GenerateCodes(int count = 10);
    string HashCode(string code);
    bool VerifyCode(string code, string hash);
}
