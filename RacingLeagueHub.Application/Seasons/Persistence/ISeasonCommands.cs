using RacingLeagueHub.Application.Seasons.Dtos;

namespace RacingLeagueHub.Application.Seasons.Persistence;

public interface ISeasonCommands
{
    Task<SeasonDto> AddAsync(CreateSeasonDto dto, CancellationToken ct = default);
    Task<long?> UpdateBySlugAsync(string leagueSlug, string seasonSlug, UpdateSeasonDto dto, CancellationToken ct = default);
    Task<int> DeleteBySlugAsync(string leagueSlug, string seasonSlug, CancellationToken ct = default);
}
