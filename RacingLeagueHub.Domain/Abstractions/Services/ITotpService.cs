namespace RacingLeagueHub.Domain.Abstractions.Services;

public interface ITotpService
{
    string GenerateSecret();
    string BuildOtpAuthUri(string issuer, string email, string secret);
    bool VerifyCode(string secret, string code, long? lastUsedTimeStep, out long matchedTimeStep);
}
