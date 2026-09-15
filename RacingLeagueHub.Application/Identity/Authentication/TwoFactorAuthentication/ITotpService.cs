namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactorAuthentication;

public interface ITotpService
{
    string GenerateSecret();
    string BuildOtpAuthUri(string issuer, string email, string secret);
    bool VerifyCode(string secret, string code, long? lastUsedTimeStep, out long matchedTimeStep);
}