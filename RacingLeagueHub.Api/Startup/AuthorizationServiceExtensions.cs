using Microsoft.AspNetCore.Authorization;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.Models.Enums;

namespace RacingLeagueHub.Api.Startup;

public static class AuthorizationServiceExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy(LeaguePolicies.LeagueOwner, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(
                    new LeaguePermissionRequirement(LeaguePermission.Owner));
            });

            options.AddPolicy(LeaguePolicies.LeagueAdmin, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(
                    new LeaguePermissionRequirement(LeaguePermission.Admin));
            });

            options.AddPolicy(LeaguePolicies.LeagueEditor, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(
                    new LeaguePermissionRequirement(LeaguePermission.Editor));
            });

            options.AddPolicy(LeaguePolicies.LeagueSteward, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(
                    new LeaguePermissionRequirement(LeaguePermission.Steward));
            });
        });

        return services;
    }
}