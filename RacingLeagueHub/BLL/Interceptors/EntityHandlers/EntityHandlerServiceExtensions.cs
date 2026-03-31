using System.Reflection;

namespace RacingLeagueHub.BLL.Interceptors.EntityHandlers;

public static class EntityHandlerServiceExtensions
{
    public static IServiceCollection AddEntityHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var targetAssemblies = assemblies.Length > 0
            ? assemblies
            : [Assembly.GetCallingAssembly()];

        var handlerTypes = targetAssemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.BaseType is { IsGenericType: true } &&
                        t.BaseType.GetGenericTypeDefinition() == typeof(EntityHandler<>));

        foreach (var handlerType in handlerTypes)
        {
            services.AddScoped(typeof(IEntityHandler), handlerType);
        }

        return services;
    }
}