using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.GrandsPrix.Persistence;

public interface IGrandPrixQueries
{
    Task<GrandPrixDto?> GetBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        CancellationToken ct = default);

    Task<PagedResult<GrandPrixDto>> GetSeasonGrandsPrixAsync(
        string leagueSlug,
        string seasonSlug,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);
}
