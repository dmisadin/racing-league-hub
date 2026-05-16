using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Domain.Abstractions;

public interface ILeagueUserRepository : IRepository<LeagueUser>
{ 
    Task<List<LeagueUser>> GetAllLeagueRolesForUser(long userId);
    Task<LeagueUser?> GetByLeagueAndUserAsync(
        long leagueId,
        long userId,
        CancellationToken cancellationToken = default);
}