using RacingLeagueHub.Application.Leagues.Dtos;

namespace RacingLeagueHub.Application.Leagues.Persistence;

public interface ILeagueCommands
{
    Task<LeagueDto> AddAsync(CreateLeagueDto dto, CancellationToken ct = default);
    Task<long?> UpdateBySlugAsync(string leagueSlug, UpdateLeagueDto dto, CancellationToken ct = default);
    Task<int> DeleteBySlugAsync(string leagueSlug, CancellationToken ct = default);
}
