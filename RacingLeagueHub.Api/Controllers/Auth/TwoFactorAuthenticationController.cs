using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.Dtos.Auth.Totp;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Abstractions.Services;

namespace RacingLeagueHub.Api.Controllers.Auth;

[Route("api/account/2fa")]
[Authorize]
public class TwoFactorAuthenticationController : BaseController
{
    private const string Issuer = "RacingLeagueHub";

    private readonly ITotpService totpService;
    private readonly IUserRepository userRepository;

    public TwoFactorAuthenticationController(ITotpService totpService,
        IUserRepository userRepository)
    {
        this.totpService = totpService;
        this.userRepository = userRepository;
    }

    [HttpPost("setup")]
    public async Task<ActionResult<TwoFactorSetupDto>> Setup(CancellationToken ct)
    {
        var userId = GetCurrentUserId();

        var user = await userRepository.GetByIdAsync(userId, user => user);

        if (user is null)
            return Unauthorized();

        if (user.TwoFactorEnabled)
            return BadRequest("Two-factor authentication is already enabled.");

        var secret = totpService.GenerateSecret();

        user.TwoFactorSecret = secret;

        await userRepository.CommitAsync(ct);

        var uri = totpService.BuildOtpAuthUri(Issuer, user.Email, secret);

        return Ok(new TwoFactorSetupDto
        {
            ManualEntryKey = secret,
            OtpAuthUri = uri
        });
    }

    [HttpPost("confirm")]
    public async Task<ActionResult> Confirm(
        ConfirmTwoFactorDto dto,
        CancellationToken ct)
    {
        var userId = GetCurrentUserId();

        var user = await userRepository.GetByIdAsync(userId, user => user);

        if (user is null)
            return Unauthorized();

        if (user.TwoFactorEnabled)
            return BadRequest("Two-factor authentication is already enabled.");

        if (string.IsNullOrWhiteSpace(user.TwoFactorSecret))
            return BadRequest("Two-factor setup has not been started.");

        var valid = totpService.VerifyCode(
            user.TwoFactorSecret,
            dto.Code,
            user.LastTotpTimeStepUsed,
            out var matchedStep
        );

        if (!valid)
            return BadRequest("Invalid authentication code.");

        user.TwoFactorEnabled = true;
        user.TwoFactorEnabledAt = DateTimeOffset.UtcNow;
        user.LastTotpTimeStepUsed = matchedStep;

        await userRepository.CommitAsync(ct);

        return NoContent();
    }
}
