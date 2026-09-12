using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RacingLeagueHub.Application.GameTeams.Persistence;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;
using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes;
using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;
using RacingLeagueHub.Application.Identity.Authentication.RefreshTokens.Persistence;
using RacingLeagueHub.Application.Identity.Authentication.Sso;
using RacingLeagueHub.Application.Leagues.Persistence;
using RacingLeagueHub.Application.LeagueUsers.Persistence;
using RacingLeagueHub.Application.Resources;
using RacingLeagueHub.Application.Resources.Persistence;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Application.Teams.Persistence;
using RacingLeagueHub.Application.TrackLayouts.Persistence;
using RacingLeagueHub.Application.Tracks.Persistence;
using RacingLeagueHub.Application.Users.Persistence;
using RacingLeagueHub.Domain.Abstractions.Repositories;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;
using RacingLeagueHub.Domain.Services.Interfaces;
using RacingLeagueHub.Identity.Authentication.Persistence;
using RacingLeagueHub.Infrastructure.Auth;
using RacingLeagueHub.Infrastructure.Auth.SSO;
using RacingLeagueHub.Infrastructure.Configuration;
using RacingLeagueHub.Infrastructure.Persistence;
using RacingLeagueHub.Infrastructure.Persistence.EntityHandlers;
using RacingLeagueHub.Infrastructure.Persistence.GameTeams;
using RacingLeagueHub.Infrastructure.Persistence.GrandsPrix;
using RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.PasswordResetTokens;
using RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.RefreshTokens;
using RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.UserRecoveryCodes;
using RacingLeagueHub.Infrastructure.Persistence.Leagues;
using RacingLeagueHub.Infrastructure.Persistence.LeagueUsers;
using RacingLeagueHub.Infrastructure.Persistence.Resources;
using RacingLeagueHub.Infrastructure.Persistence.Seasons;
using RacingLeagueHub.Infrastructure.Persistence.Teams;
using RacingLeagueHub.Infrastructure.Persistence.TrackLayouts;
using RacingLeagueHub.Infrastructure.Persistence.Users;
using RacingLeagueHub.Infrastructure.Repositories;
using RacingLeagueHub.Infrastructure.Services;
using System.Reflection;

namespace RacingLeagueHub.Infrastructure;

public static class InfrastructureServiceRegistration
{

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AdventureContext>(options =>
                            options.UseNpgsql(configuration
                                    .GetConnectionString("DefaultConnection"))
                                    .UseSnakeCaseNamingConvention());

        services.AddDbContext<RacingContext>(options =>
                    options.UseNpgsql(configuration
                            .GetConnectionString("DefaultConnection"))
                            .UseSnakeCaseNamingConvention());

        return services;
    }

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

    public static IServiceCollection AddAwsStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var s3ConfigurationSection = configuration.GetSection("S3");
        services.Configure<S3Options>(s3ConfigurationSection);

        services.AddAWSService<IAmazonS3>();

        services.AddScoped<IResourceQueries, ResourceQueries>();

        services.AddScoped<IStorageService, S3StorageService>();
        services.AddScoped<IResourceService, ResourceService>();

        return services;
    }

    public static IServiceCollection AddEntityHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var targetAssemblies = assemblies.Length > 0
            ? assemblies
            : [typeof(InfrastructureServiceRegistration).Assembly];

        var handlerTypes = targetAssemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t =>
                t.BaseType is { IsGenericType: true } 
                && t.BaseType.GetGenericTypeDefinition() == typeof(EntityHandler<>));

        foreach (var handlerType in handlerTypes)
        {
            services.AddScoped(typeof(IEntityHandler), handlerType);
        }

        return services;
    }

    public static IServiceCollection AddQueriesAndCommands(this IServiceCollection services)
    {
        services.AddScoped<ITeamQueries, TeamQueries>();
        services.AddScoped<ITeamCommands, TeamCommands>();
        services.AddScoped<IGameTeamQueries, GameTeamQueries>();
        services.AddScoped<IGameTeamCommands, GameTeamCommands>();
        services.AddScoped<ITrackQueries, TrackQueries>();
        services.AddScoped<ITrackCommands, TrackCommands>();
        services.AddScoped<ITrackLayoutQueries, TrackLayoutQueries>();
        services.AddScoped<ITrackLayoutCommands, TrackLayoutCommands>();
        services.AddScoped<IResourceQueries, ResourceQueries>();
        services.AddScoped<IResourceCommands, ResourceCommands>();
        services.AddScoped<IGrandPrixCommands, GrandPrixCommands>();
        services.AddScoped<IGrandPrixQueries, GrandPrixQueries>();
        services.AddScoped<ISeasonCommands, SeasonCommands>();
        services.AddScoped<ISeasonQueries, SeasonQueries>();
        services.AddScoped<ILeagueUserQueries, LeagueUserQueries>();
        services.AddScoped<ILeagueQueries, LeagueQueries>();
        services.AddScoped<IUserQueries, UserQueries>();
        services.AddScoped<IUserCommands, UserCommands>();
        services.AddScoped<IRefreshTokenQueries, RefreshTokenQueries>();
        services.AddScoped<IRefreshTokenCommands, RefreshTokenCommands>();
        services.AddScoped<IPasswordResetTokenQueries, PasswordResetTokenQueries>();
        services.AddScoped<IPasswordResetTokenCommands, PasswordResetTokenCommands>();
        services.AddScoped<IUserRecoveryCodeQueries, UserRecoveryCodeQueries>();
        services.AddScoped<IUserRecoveryCodeCommands, UserRecoveryCodeCommands>();

        return services;
    }
}