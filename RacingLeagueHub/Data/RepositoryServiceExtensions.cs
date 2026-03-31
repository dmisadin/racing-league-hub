using RacingLeagueHub.BLL.Infrastructure;
using RacingLeagueHub.Data.Repositories;
using System.Reflection;

namespace RacingLeagueHub.Data;

public static class RepositoryServiceExtensions
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

        return services;
    }
}