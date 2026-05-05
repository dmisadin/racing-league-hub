using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Api.Controllers;

public class BaseController : ControllerBase
{
    protected long GetCurrentUserId()
    {
        var encryptedUserId = User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
        var encryptedId = new EncryptedId(encryptedUserId);

        return encryptedId.RawId;
    }
}