using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.GrandsPrix.Persistence;

public interface IGrandPrixQueries
{
    Task<GrandPrixDto?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<GrandPrixDto?> GetBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        string grandPrixSlug,
        CancellationToken ct = default);

    Task<PagedResult<GrandPrixDto>> GetSeasonGrandsPrixAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);
}
