using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RacingLeagueHub.Application.Services.Identity;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Services;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterAppLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}
