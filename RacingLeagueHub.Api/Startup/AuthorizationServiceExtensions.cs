using Microsoft.AspNetCore.Authorization;

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
        });

        return services;
    }
}