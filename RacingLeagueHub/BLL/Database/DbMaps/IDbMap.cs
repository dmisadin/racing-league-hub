using Microsoft.EntityFrameworkCore;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public interface IDbMap
{
    void Initialize(ModelBuilder modelBuilder);
}
