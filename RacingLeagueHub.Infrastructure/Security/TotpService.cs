using OtpNet;
using RacingLeagueHub.Domain.Abstractions.Services;

namespace RacingLeagueHub.Infrastructure.Security;

internal class TotpService : ITotpService
{
    public string GenerateSecret()
    {
        var key = KeyGeneration.GenerateRandomKey(20);
        return Base32Encoding.ToString(key);
    }

    public string BuildOtpAuthUri(string issuer, string email, string secret)
    {
        var encodedIssuer = Uri.EscapeDataString(issuer);
        var encodedEmail = Uri.EscapeDataString(email);

        return $"otpauth://totp/{encodedIssuer}:{encodedEmail}" +
               $"?secret={secret}" +
               $"&issuer={encodedIssuer}" +
               $"&digits=6" +
               $"&period=30";
    }

    public bool VerifyCode(
        string secret,
        string code,
        long? lastUsedTimeStep,
        out long matchedTimeStep)
    {
        matchedTimeStep = 0;

        if (string.IsNullOrWhiteSpace(secret))
            return false;

        code = code.Replace(" ", "").Trim();

        if (code.Length != 6 || !code.All(char.IsDigit))
            return false;

        var secretBytes = Base32Encoding.ToBytes(secret);
        var totp = new Totp(secretBytes, step: 30, totpSize: 6);

        var isValid = totp.VerifyTotp(
            code,
            out matchedTimeStep,
            new VerificationWindow(previous: 1, future: 1)
        );

        if (!isValid)
            return false;

        // Prevent reusing the same code in the same time step.
        if (lastUsedTimeStep.HasValue && lastUsedTimeStep.Value == matchedTimeStep)
            return false;

        return true;
    }
}
