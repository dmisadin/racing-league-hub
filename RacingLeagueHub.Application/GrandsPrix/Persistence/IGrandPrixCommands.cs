using RacingLeagueHub.Application.GrandsPrix.Dtos;

namespace RacingLeagueHub.Application.GrandsPrix.Persistence;

public interface IGrandPrixCommands
{
    Task<int> AddAsync(CreateGrandPrixDto dto, CancellationToken ct);

    Task<int?> UpdateBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        string grandPrixSlug,
        UpdateGrandPrixDto dto,
        CancellationToken ct = default);

    Task<int> DeleteBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        string grandPrixSlug,
        CancellationToken ct = default);
}
