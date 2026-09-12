using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes;
using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;
using RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Dtos;
using RacingLeagueHub.Application.Users.Persistence;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactorAuthentication;

public class TwoFactorService : ITwoFactorService
{
    private const string Issuer = "RacingLeagueHub";

    private readonly IUserQueries userQueries;
    private readonly IUserCommands userCommands;
    private readonly ITotpService totpService;
    private readonly IRecoveryCodeService recoveryCodeService;
    private readonly IUserRecoveryCodeQueries userRecoveryCodeQueries;
    private readonly IUserRecoveryCodeCommands userRecoveryCodeCommands;

    public TwoFactorService(
        IUserQueries userQueries,
        IUserCommands userCommands,
        ITotpService totpService,
        IRecoveryCodeService recoveryCodeService,
        IUserRecoveryCodeQueries userRecoveryCodeQueries,
        IUserRecoveryCodeCommands userRecoveryCodeCommands)
    {
        this.userQueries = userQueries;
        this.userCommands = userCommands;
        this.totpService = totpService;
        this.recoveryCodeService = recoveryCodeService;
        this.userRecoveryCodeQueries = userRecoveryCodeQueries;
        this.userRecoveryCodeCommands = userRecoveryCodeCommands;
    }

    public async Task<TwoFactorSetupDto> StartSetupAsync(long userId, CancellationToken ct = default)
    {
        var user = await userQueries.GetUserAsync(userId, ct)
            ?? throw new UnauthorizedAccessException();

        if (user.TwoFactorEnabled)
            throw new InvalidOperationException("Two-factor authentication is already enabled.");

        var secret = totpService.GenerateSecret();

        user.TwoFactorSecret = secret;

        await userCommands.SaveChangesAsync(ct);

        var uri = totpService.BuildOtpAuthUri(
            Issuer,
            user.Email,
            secret
        );

        return new TwoFactorSetupDto
        {
            ManualEntryKey = secret,
            OtpAuthUri = uri
        };
    }

    public async Task<ConfirmTwoFactorResponse> ConfirmSetupAsync(long userId, string code, CancellationToken ct = default)
    {
        var user = await userQueries.GetUserAsync(userId, ct)
            ?? throw new UnauthorizedAccessException();

        if (user.TwoFactorEnabled)
            throw new InvalidOperationException("Two-factor authentication is already enabled.");

        if (string.IsNullOrWhiteSpace(user.TwoFactorSecret))
            throw new InvalidOperationException("Two-factor setup has not been started.");

        var valid = totpService.VerifyCode(
            user.TwoFactorSecret,
            code,
            user.LastTotpTimeStepUsed,
            out var matchedStep
        );

        if (!valid)
            throw new InvalidOperationException("Invalid authentication code.");

        user.TwoFactorEnabled = true;
        user.TwoFactorEnabledAt = DateTimeOffset.UtcNow;
        user.LastTotpTimeStepUsed = matchedStep;

        IReadOnlyList<string> recoveryCodes = recoveryCodeService.GenerateCodes(10);
        List<UserRecoveryCode> userRecoveryCodes = new List<UserRecoveryCode>();

        foreach (var recoveryCode in recoveryCodes)
        {
            userRecoveryCodes.Add(new UserRecoveryCode
            {
                UserId = user.Id,
                CodeHash = recoveryCodeService.HashCode(recoveryCode),
                CreatedAt = DateTime.UtcNow
            });
        }

        await userRecoveryCodeCommands.AddRangeAsync(userRecoveryCodes, ct);

        await userCommands.SaveChangesAsync(ct);

        return new ConfirmTwoFactorResponse(recoveryCodes);
    }

    public async Task<RecoveryCodesResponse> RegenerateRecoveryCodesAsync(long userId, CancellationToken ct = default)
    {
        var user = await userQueries.GetUserAsync(userId, ct)
            ?? throw new UnauthorizedAccessException();

        if (!user.TwoFactorEnabled)
            throw new InvalidOperationException("Two-factor authentication is not enabled.");

        await userRecoveryCodeCommands.DeleteForUserAsync(userId, ct);

        IReadOnlyList<string> recoveryCodes = recoveryCodeService.GenerateCodes(10);
        List<UserRecoveryCode> userRecoveryCodes = new List<UserRecoveryCode>();

        foreach (var recoveryCode in recoveryCodes)
        {
            userRecoveryCodes.Add(new UserRecoveryCode
            {
                UserId = user.Id,
                CodeHash = recoveryCodeService.HashCode(recoveryCode),
                CreatedAt = DateTime.UtcNow
            });
        }

        await userRecoveryCodeCommands.AddRangeAsync(userRecoveryCodes, ct);

        await userCommands.SaveChangesAsync(ct);

        return new RecoveryCodesResponse(recoveryCodes);
    }
}