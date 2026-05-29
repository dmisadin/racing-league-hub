using RacingLeagueHub.Application.Dtos.Auth.Totp;
using RacingLeagueHub.Application.Dtos.Auth.TwoFactor;

namespace RacingLeagueHub.Application.Services.TwoFactorAuthentication;

public interface ITwoFactorService
{
    Task<TwoFactorSetupDto> StartSetupAsync(long userId, CancellationToken ct = default);
    Task<ConfirmTwoFactorResponse> ConfirmSetupAsync(long userId, string code, CancellationToken ct = default);
    Task<RecoveryCodesResponse> RegenerateRecoveryCodesAsync(long userId, CancellationToken ct = default);
}