using RacingLeagueHub.Application.GrandsPrix.Dtos;

namespace RacingLeagueHub.Application.GrandsPrix.Persistence;

public interface IGrandPrixCommands
{
    Task<GrandPrixDto> AddAsync(CreateGrandPrixDto dto, CancellationToken ct);
    Task<long?> UpdateBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        UpdateGrandPrixDto dto,
        CancellationToken ct = default);

    Task<int> DeleteBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        CancellationToken ct = default);
}
