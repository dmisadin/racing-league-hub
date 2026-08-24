using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Services.Identity;
using RacingLeagueHub.Application.Services.TeamService;
using RacingLeagueHub.Application.Services.TwoFactorAuthentication;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<ITwoFactorService, TwoFactorService>();

        services.AddBusinessLogicServices();

        services.AddDtoMappers();

        return services;
    }

    private static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<ITeamService, TeamService>();

        return services;
    }

    private static IServiceCollection AddDtoMappers(this IServiceCollection services)
    {
        var mapperInterface = typeof(IDtoMapper<,>);
        var applicationAssembly = typeof(LeagueDtoMapper).Assembly;

        var mapperTypes = applicationAssembly
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false })
            .Select(type => new
            {
                Implementation = type,
                Service = type.GetInterfaces()
                    .SingleOrDefault(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == mapperInterface)
            })
            .Where(x => x.Service is not null);

        foreach (var mapper in mapperTypes)
        {
            services.AddScoped(mapper.Service!, mapper.Implementation);
        }

        return services;
    }
}
