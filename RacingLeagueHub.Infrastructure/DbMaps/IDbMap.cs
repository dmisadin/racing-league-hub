using Microsoft.EntityFrameworkCore;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public interface IDbMap
{
    void Initialize(ModelBuilder modelBuilder);
}
