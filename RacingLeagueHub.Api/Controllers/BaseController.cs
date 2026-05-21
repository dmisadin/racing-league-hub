using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RacingLeagueHub.Application.Models;
using System.Security.Claims;

namespace RacingLeagueHub.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected long GetCurrentUserId()
    {
        var encryptedUserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrWhiteSpace(encryptedUserId))
            throw new UnauthorizedAccessException("User ID claim is missing.");

        var encryptedId = new EncryptedId(encryptedUserId);

        return encryptedId.RawId;
    }
}