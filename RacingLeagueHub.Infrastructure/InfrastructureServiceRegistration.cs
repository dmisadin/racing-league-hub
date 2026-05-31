using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RacingLeagueHub.Application.Services.Abstractions;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Abstractions.Admin;
using RacingLeagueHub.Domain.Abstractions.Repositories;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;
using RacingLeagueHub.Infrastructure.Auth;
using RacingLeagueHub.Infrastructure.Auth.SSO;
using RacingLeagueHub.Infrastructure.Auth.SSO.Models;
using RacingLeagueHub.Infrastructure.Repositories;
using RacingSeasonHub.Infrastructure.Repositories;
using System.Reflection;

namespace RacingLeagueHub.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        var targetAssemblies = assemblies.Length > 0
            ? assemblies
            : [Assembly.GetCallingAssembly()];

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        var concreteRepos = targetAssemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.BaseType is { IsGenericType: true } &&
                        t.BaseType.GetGenericTypeDefinition() == typeof(GenericRepository<>));

        foreach (var repoType in concreteRepos)
        {
            var entityType = repoType.BaseType!.GetGenericArguments()[0];
            var serviceType = typeof(IRepository<>).MakeGenericType(entityType);
            services.AddScoped(serviceType, repoType);
            services.AddScoped(repoType);
        }

        services.AddScoped<ILeagueRepository, LeagueRepository>();
        services.AddScoped<ISeasonRepository, SeasonRepository>();
        services.AddScoped<IGrandPrixRepository, GrandPrixRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
        services.AddScoped<ILeagueUserRepository, LeagueUserRepository>();

        services.AddScoped<ITrackLayoutRepository, TrackLayoutRepository>();
        services.AddScoped<IUserRecoveryCodeRepository, UserRecoveryCodeRepository>();
        services.AddScoped<IUserExternalLoginRepository, UserExternalLoginRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITotpService, TotpService>();
        services.AddScoped<IPasswordHasher<UserRecoveryCode>, PasswordHasher<UserRecoveryCode>>();
        services.AddScoped<IRecoveryCodeService, RecoveryCodeService>();

        services.Configure<GoogleAuthOptions>(configuration.GetSection("Authentication:Google"));
        services.AddHttpClient<IGoogleOAuthService, GoogleOAuthService>();
        services.AddScoped<ISsoStateService, SsoStateService>();

        return services;
    }
}