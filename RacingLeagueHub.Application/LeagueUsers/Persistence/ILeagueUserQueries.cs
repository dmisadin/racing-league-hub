using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.LeagueUsers.Persistence;

public interface ILeagueUserQueries
{ 
    Task<List<LeagueUser>> GetAllLeagueRolesForUserAsync(int userId, CancellationToken cancellationToken = default);
    Task<LeagueUser?> GetByLeagueAndUserAsync(int leagueId, int userId, CancellationToken cancellationToken = default);
    Task<LeagueUser?> GetByLeagueAndUserAsync(string leagueSlug, int userId, CancellationToken cancellationToken = default);
}