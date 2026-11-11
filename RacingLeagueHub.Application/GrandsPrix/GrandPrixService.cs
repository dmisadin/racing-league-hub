using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Application.Seasons.Persistence;

namespace RacingLeagueHub.Application.GrandsPrix;

internal class GrandPrixService : IGrandPrixService
{
    private readonly IGrandPrixQueries grandPrixQueries;
    private readonly IGrandPrixCommands grandPrixCommands;
    private readonly ISeasonQueries seasonQueries;

    public GrandPrixService(
        IGrandPrixQueries grandPrixQueries,
        IGrandPrixCommands grandPrixCommands,
        ISeasonQueries seasonQueries)
    {
        this.grandPrixQueries = grandPrixQueries;
        this.grandPrixCommands = grandPrixCommands;
        this.seasonQueries = seasonQueries;
    }

    public async Task<GrandPrixDto?> CreateAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        CreateGrandPrixDto dto,
        CancellationToken ct = default)
    {
        var lookup = await seasonQueries.GetDivisionLookupAsync(leagueSlug, seasonSlug, ct);

        if (lookup is null)
            return null;

        var divisions = lookup.Divisions;

        int? seasonDivisionId;

        if (divisionSlug is null)
        {
            if (divisions.Count > 1)
                throw new InvalidOperationException(
                    "This season has multiple divisions. Add the Grand Prix to a specific division.");

            seasonDivisionId = divisions.SingleOrDefault()?.SeasonDivisionId;
        }
        else
        {
            var division = divisions.FirstOrDefault(d => d.DivisionSlug == divisionSlug);

            if (division is null)
                return null;

            seasonDivisionId = division.SeasonDivisionId;
        }

        dto.SeasonId = lookup.SeasonId;
        dto.SeasonDivisionId = seasonDivisionId;

        var id = await grandPrixCommands.AddAsync(dto, ct);

        return await grandPrixQueries.GetByIdAsync(id, ct);
    }
}
