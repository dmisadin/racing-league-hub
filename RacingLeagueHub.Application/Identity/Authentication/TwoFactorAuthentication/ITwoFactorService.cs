using RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Dtos;

namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactorAuthentication;

public interface ITwoFactorService
{
    Task<TwoFactorSetupDto> StartSetupAsync(long userId, CancellationToken ct = default);
    Task<ConfirmTwoFactorResponse> ConfirmSetupAsync(long userId, string code, CancellationToken ct = default);
    Task<RecoveryCodesResponse> RegenerateRecoveryCodesAsync(long userId, CancellationToken ct = default);
}