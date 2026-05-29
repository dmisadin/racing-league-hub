using RacingLeagueHub.Application.Dtos.Auth.Totp;
using RacingLeagueHub.Application.Dtos.Auth.TwoFactor;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Abstractions.Repositories;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Services.TwoFactorAuthentication;

public class TwoFactorService : ITwoFactorService
{
    private const string Issuer = "RacingLeagueHub";

    private readonly IUserRepository userRepository;
    private readonly ITotpService totpService;
    private readonly IRecoveryCodeService recoveryCodeService;
    private readonly IUserRecoveryCodeRepository userRecoveryCodeRepository;

    public TwoFactorService(
        IUserRepository userRepository,
        ITotpService totpService,
        IRecoveryCodeService recoveryCodeService,
        IUserRecoveryCodeRepository userRecoveryCodeRepository)
    {
        this.userRepository = userRepository;
        this.totpService = totpService;
        this.recoveryCodeService = recoveryCodeService;
        this.userRecoveryCodeRepository = userRecoveryCodeRepository;
    }

    public async Task<TwoFactorSetupDto> StartSetupAsync(long userId, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(userId, ct)
            ?? throw new UnauthorizedAccessException();

        if (user.TwoFactorEnabled)
            throw new InvalidOperationException("Two-factor authentication is already enabled.");

        var secret = totpService.GenerateSecret();

        user.TwoFactorSecret = secret;

        await userRepository.CommitAsync(ct);

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
        var user = await userRepository.GetByIdAsync(userId, ct)
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

        var recoveryCodes = recoveryCodeService.GenerateCodes(10);

        foreach (var recoveryCode in recoveryCodes)
        {
            await userRecoveryCodeRepository.InsertAsync(new UserRecoveryCode
            {
                UserId = user.Id,
                CodeHash = recoveryCodeService.HashCode(recoveryCode),
                CreatedAt = DateTime.UtcNow
            });
        }

        await userRepository.CommitAsync(ct);

        return new ConfirmTwoFactorResponse(recoveryCodes);
    }

    public async Task<RecoveryCodesResponse> RegenerateRecoveryCodesAsync(long userId, CancellationToken ct = default)
    {
        var user = await userRepository.GetByIdAsync(userId, ct)
            ?? throw new UnauthorizedAccessException();

        if (!user.TwoFactorEnabled)
            throw new InvalidOperationException("Two-factor authentication is not enabled.");

        await userRecoveryCodeRepository.DeleteForUserAsync(userId, ct);

        var recoveryCodes = recoveryCodeService.GenerateCodes(10);

        foreach (var recoveryCode in recoveryCodes)
        {
            await userRecoveryCodeRepository.InsertAsync(new UserRecoveryCode
            {
                UserId = userId,
                CodeHash = recoveryCodeService.HashCode(recoveryCode),
                CreatedAt = DateTime.UtcNow
            });
        }

        await userRepository.CommitAsync(ct);

        return new RecoveryCodesResponse(recoveryCodes);
    }
}