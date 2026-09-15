using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace RacingLeagueHub.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected int? CurrentUserId
    {
        get
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return int.TryParse(userId, out var id)
                ? id
                : null;
        }
    }

    protected int GetRequiredUserId()
    {
        var userId = CurrentUserId;

        if (userId is null)
            throw new InvalidOperationException(
                "The current authenticated user does not have a valid user ID.");

        return userId.Value;
    }
}