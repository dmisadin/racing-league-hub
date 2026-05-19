using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Models.Enums;
using RacingLeagueHub.Domain.Abstractions;
using System.Security.Claims;

namespace RacingLeagueHub.Api.Authorization;

public sealed class LeaguePermissionHandler
    : AuthorizationHandler<LeaguePermissionRequirement>
{
    private readonly ILeagueUserRepository leagueUserRepository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LeaguePermissionHandler(
        ILeagueUserRepository leagueUserRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        this.leagueUserRepository = leagueUserRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        LeaguePermissionRequirement requirement)
    {
        var httpContext = this.httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;

        var userId = GetUserId(context.User);

        if (userId is null)
            return;

        var leagueSlug = GetLeagueSlugFromRoute(httpContext);

        if (leagueSlug is null)
            return;

        var leagueUser = await this.leagueUserRepository.GetByLeagueAndUserAsync(
            leagueSlug,
            userId.Value,
            httpContext.RequestAborted);

        if (leagueUser is null)
            return;

        var allowed = requirement.Permission switch
        {
            LeaguePermission.Owner => leagueUser.CanOwnLeague(),

            LeaguePermission.Admin => leagueUser.CanManageLeague(),

            LeaguePermission.Editor => leagueUser.CanEditLeague(),

            LeaguePermission.Steward => leagueUser.CanStewardLeague(),

            _ => false
        };

        if (allowed)
        {
            context.Succeed(requirement);
        }
    }

    private static long? GetUserId(ClaimsPrincipal user)
    {
        var encryptedUserId = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrWhiteSpace(encryptedUserId))
            return null;

        try
        {
            var encryptedId = new EncryptedId(encryptedUserId);
            return encryptedId.RawId;
        }
        catch
        {
            return null;
        }
    }

    private static string? GetLeagueSlugFromRoute(HttpContext httpContext)
    {
        return httpContext.Request.RouteValues["leagueSlug"]?.ToString();
    }

    private static long? GetLeagueIdFromRoute(HttpContext httpContext)
    {
        var leagueIdValue = httpContext.Request.RouteValues["leagueId"]?.ToString();

        if (long.TryParse(leagueIdValue, out var leagueId))
            return leagueId;

        return null;
    }
}