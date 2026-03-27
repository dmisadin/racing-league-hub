using Microsoft.EntityFrameworkCore;

namespace RacingLeagueHub.Data.DbMaps;

public interface IDbMap
{
    void Initialize(ModelBuilder modelBuilder);
}
