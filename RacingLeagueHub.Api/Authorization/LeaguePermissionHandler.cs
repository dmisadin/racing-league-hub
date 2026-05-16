using Microsoft.AspNetCore.Authorization;
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

        var leagueId = GetLeagueIdFromRoute(httpContext);

        if (leagueId is null)
            return;

        var leagueUser = await this.leagueUserRepository.GetByLeagueAndUserAsync(
            leagueId.Value,
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
        var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (long.TryParse(userIdValue, out var userId))
            return userId;

        return null;
    }

    private static long? GetLeagueIdFromRoute(HttpContext httpContext)
    {
        var leagueIdValue = httpContext.Request.RouteValues["leagueId"]?.ToString();

        if (long.TryParse(leagueIdValue, out var leagueId))
            return leagueId;

        return null;
    }
}