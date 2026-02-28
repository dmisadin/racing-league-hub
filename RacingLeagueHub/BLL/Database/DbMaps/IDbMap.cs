using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.BLL.Database.DbMaps;

public interface IDbMap
{
    void Initialize(ModelBuilder modelBuilder);
}
