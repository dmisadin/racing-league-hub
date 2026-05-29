using Microsoft.AspNetCore.Identity;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;
using System.Security.Cryptography;

namespace RacingLeagueHub.Infrastructure.Security;

public class RecoveryCodeService : IRecoveryCodeService
{
    private readonly IPasswordHasher<UserRecoveryCode> passwordHasher;

    public RecoveryCodeService(IPasswordHasher<UserRecoveryCode> passwordHasher)
    {
        this.passwordHasher = passwordHasher;
    }

    public IReadOnlyList<string> GenerateCodes(int count = 10)
    {
        var codes = new List<string>();

        for (var i = 0; i < count; i++)
        {
            codes.Add(GenerateSingleCode());
        }

        return codes;
    }

    public string HashCode(string code)
    {
        var normalized = Normalize(code);
        return passwordHasher.HashPassword(new UserRecoveryCode(), normalized);
    }

    public bool VerifyCode(string code, string hash)
    {
        var normalized = Normalize(code);

        var result = passwordHasher.VerifyHashedPassword(
            new UserRecoveryCode(),
            hash,
            normalized
        );

        return result != PasswordVerificationResult.Failed;
    }

    private static string GenerateSingleCode()
    {
        const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        Span<char> chars = stackalloc char[14];

        for (var i = 0; i < chars.Length; i++)
        {
            if (i == 4 || i == 9)
            {
                chars[i] = '-';
                continue;
            }

            chars[i] = alphabet[RandomNumberGenerator.GetInt32(alphabet.Length)];
        }

        return new string(chars);
    }

    private static string Normalize(string code)
    {
        return code
            .Trim()
            .Replace("-", "")
            .Replace(" ", "")
            .ToUpperInvariant();
    }
}
