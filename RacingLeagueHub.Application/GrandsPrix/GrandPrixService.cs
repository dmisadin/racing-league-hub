using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Application.Seasons.Dtos;
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

        SeasonDivisionRefDto? division;

        if (divisionSlug is null)
        {
            if (divisions.Count > 1)
                throw new InvalidOperationException(
                    "This season has multiple divisions. Add the Grand Prix to a specific division.");

            division = divisions.SingleOrDefault();
        }
        else
        {
            division = divisions.FirstOrDefault(d => d.DivisionSlug == divisionSlug);
        }

        if (division is null)
            return null;

        dto.SeasonDivisionId = division.SeasonDivisionId;

        var id = await grandPrixCommands.AddAsync(dto, ct);

        return await grandPrixQueries.GetByIdAsync(id, ct);
    }
}
