using Microsoft.AspNetCore.Authorization;
using RacingLeagueHub.Application.Models.Enums;

public sealed class LeaguePermissionRequirement : IAuthorizationRequirement
{
    public LeaguePermission Permission { get; }

    public LeaguePermissionRequirement(LeaguePermission permission)
    {
        Permission = permission;
    }
}