using RacingLeagueHub.Application.GrandsPrix.Dtos;

namespace RacingLeagueHub.Application.GrandsPrix;

public interface IGrandPrixService
{
    /// <summary>
    /// Creates a Grand Prix in the season and assigns it to the division given by <paramref name="divisionSlug"/>.
    /// Without a division slug, a season with exactly one division gets that division assigned and a season
    /// without divisions leaves it empty; a season with several divisions needs the division to be specified.
    /// </summary>
    /// <returns><c>null</c> when the season or division does not exist.</returns>
    /// <exception cref="InvalidOperationException">The season has several divisions and none was given.</exception>
    Task<GrandPrixDto?> CreateAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        CreateGrandPrixDto dto,
        CancellationToken ct = default);
}
