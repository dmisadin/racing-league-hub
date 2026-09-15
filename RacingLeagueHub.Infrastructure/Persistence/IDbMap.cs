using Microsoft.EntityFrameworkCore;

namespace RacingLeagueHub.Infrastructure.Persistence;

public interface IDbMap
{
    void Initialize(ModelBuilder modelBuilder);
}
