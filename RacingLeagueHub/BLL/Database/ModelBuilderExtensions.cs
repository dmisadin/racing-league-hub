using RacingLeagueHub.BLL.Database.DbMaps;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RacingLeagueHub.BLL.Database;

public static class ModelBuilderExtensions
{
    public static void ApplyDbMapsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
    {
        var mapTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
            .Where(t => typeof(IDbMap).IsAssignableFrom(t));

        foreach (var mapType in mapTypes)
        {
            if (Activator.CreateInstance(mapType) is IDbMap map)
            {
                map.Initialize(modelBuilder);
            }
        }
    }
}
