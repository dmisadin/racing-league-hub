using RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Dtos;

namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactorAuthentication;

public interface ITwoFactorService
{
    Task<TwoFactorSetupDto> StartSetupAsync(int userId, CancellationToken ct = default);
    Task<ConfirmTwoFactorResponse> ConfirmSetupAsync(int userId, string code, CancellationToken ct = default);
    Task<RecoveryCodesResponse> RegenerateRecoveryCodesAsync(int userId, CancellationToken ct = default);
}