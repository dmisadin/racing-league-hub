using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Dtos;
using RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Models;
using RacingLeagueHub.Application.Identity.Authentication.TwoFactorAuthentication;

namespace RacingLeagueHub.Api.Controllers.Auth;

[Route("api/account/2fa")]
[Authorize]
public class TwoFactorAuthenticationController : ApiController
{
    private readonly ITwoFactorService twoFactorService;

    public TwoFactorAuthenticationController(ITwoFactorService twoFactorService)
    {
        this.twoFactorService = twoFactorService;
    }

    [HttpPost("setup")]
    public async Task<ActionResult<TwoFactorSetupDto>> Setup(CancellationToken ct)
    {
        var userId = GetCurrentUserId();

        var result = await twoFactorService.StartSetupAsync(userId, ct);

        return Ok(result);
    }

    [HttpPost("confirm")]
    public async Task<ActionResult<ConfirmTwoFactorResponse>> Confirm(ConfirmTwoFactorDto dto,CancellationToken ct)
    {
        var userId = GetCurrentUserId();

        var result = await twoFactorService.ConfirmSetupAsync(userId, dto.Code, ct);

        return Ok(result);
    }

    [HttpPost("recovery-codes/regenerate")]
    public async Task<ActionResult<RecoveryCodesResponse>> RegenerateRecoveryCodes(CancellationToken ct)
    {
        var userId = GetCurrentUserId();

        var result = await twoFactorService.RegenerateRecoveryCodesAsync(userId, ct);

        return Ok(result);
    }
}