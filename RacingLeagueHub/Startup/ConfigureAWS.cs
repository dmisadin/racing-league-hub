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
        services.Configure<S3Options>(configuration.GetSection("S3"));

        services.AddAWSService<IAmazonS3>();

        services.AddScoped<IResourceRepository, ResourceRepository>();

        services.AddScoped<IStorageService, S3StorageService>();
        services.AddScoped<IResourceService, ResourceService>();

        return services;
    }
}