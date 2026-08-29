using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RacingLeagueHub.Application.Services.Abstractions;
using RacingLeagueHub.Application.Services.GameTeamService.Persistence;
using RacingLeagueHub.Application.Services.ResourceService;
using RacingLeagueHub.Application.Services.ResourceService.Persistence;
using RacingLeagueHub.Application.Services.TeamService.Persistence;
using RacingLeagueHub.Application.Services.TrackService.Persistence;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Abstractions.Admin;
using RacingLeagueHub.Domain.Abstractions.Repositories;
using RacingLeagueHub.Domain.Abstractions.Services;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;
using RacingLeagueHub.Domain.Services.Interfaces;
using RacingLeagueHub.Infrastructure.Auth;
using RacingLeagueHub.Infrastructure.Auth.SSO;
using RacingLeagueHub.Infrastructure.Configuration;
using RacingLeagueHub.Infrastructure.Persistence;
using RacingLeagueHub.Infrastructure.Persistence.EntityHandlers;
using RacingLeagueHub.Infrastructure.Persistence.GameTeams;
using RacingLeagueHub.Infrastructure.Persistence.Resources;
using RacingLeagueHub.Infrastructure.Persistence.Teams;
using RacingLeagueHub.Infrastructure.Repositories;
using RacingLeagueHub.Infrastructure.Services;
using RacingSeasonHub.Infrastructure.Repositories;
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

    public static IServiceCollection AddAwsStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var s3ConfigurationSection = configuration.GetSection("S3");
        services.Configure<S3Options>(s3ConfigurationSection);

        services.AddAWSService<IAmazonS3>();

        services.AddScoped<IResourceRepository, ResourceRepository>();

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
        services.AddScoped<IResourceQueries, ResourceQueries>();
        services.AddScoped<IResourceCommands, ResourceCommands>();

        return services;
    }
}