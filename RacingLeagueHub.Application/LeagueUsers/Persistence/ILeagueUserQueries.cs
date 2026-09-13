using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.LeagueUsers.Persistence;

public interface ILeagueUserQueries
{ 
    Task<List<LeagueUser>> GetAllLeagueRolesForUserAsync(long userId, CancellationToken cancellationToken = default);
    Task<LeagueUser?> GetByLeagueAndUserAsync(long leagueId, long userId, CancellationToken cancellationToken = default);
    Task<LeagueUser?> GetByLeagueAndUserAsync(string leagueSlug, long userId, CancellationToken cancellationToken = default);
}