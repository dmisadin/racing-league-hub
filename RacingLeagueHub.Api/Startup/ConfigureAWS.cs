using Amazon.S3;
using RacingLeagueHub.Application.Services;
using RacingLeagueHub.Application.Storage;
using RacingLeagueHub.Domain.Infrastructure;
using RacingLeagueHub.Domain.Services.Interfaces;
using RacingLeagueHub.Infrastructure.Repositories;
using RacingLeagueHub.Infrastructure.Services;

namespace RacingLeagueHub.Api.Startup;

public static class ConfigureAWS
{
    public static IServiceCollection RegisterAWSServices(this IServiceCollection services, IConfiguration configuration)
    {
        var s3ConfigurationSection = configuration.GetSection("S3");
        S3Settings.Initialize(s3ConfigurationSection.Get<S3Options>()!);
        services.Configure<S3Options>(s3ConfigurationSection);

        services.AddAWSService<IAmazonS3>();

        services.AddScoped<IResourceRepository, ResourceRepository>();

        services.AddScoped<IStorageService, S3StorageService>();
        services.AddScoped<IResourceService, ResourceService>();

        return services;
    }
}