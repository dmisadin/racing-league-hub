using Amazon.S3;
using RacingLeagueHub.BLL.Models.Storage;
using RacingLeagueHub.BLL.Services;
using RacingLeagueHub.BLL.Services.Interfaces;
using RacingLeagueHub.Data.Repositories;

namespace RacingLeagueHub.Startup;

public static class ConfigureAWS
{
    public static IServiceCollection RegisterAWSServices(this IServiceCollection services, IConfiguration configuration)
    {
        var s3ConfigurationSection = configuration.GetSection("S3");
        S3Settings.Initialize(s3ConfigurationSection);
        services.Configure<S3Options>(s3ConfigurationSection);

        services.AddAWSService<IAmazonS3>();

        services.AddScoped<IResourceRepository, ResourceRepository>();

        services.AddScoped<IStorageService, S3StorageService>();
        services.AddScoped<IResourceService, ResourceService>();

        return services;
    }
}