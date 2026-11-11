using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Domain.GrandsPrix;

namespace RacingLeagueHub.Infrastructure.Persistence.GrandsPrix;

internal class GrandPrixCommands : IGrandPrixCommands
{
    private readonly RacingContext racingContext;

    public GrandPrixCommands(RacingContext racingContext)
    {
        this.racingContext = racingContext;
    }

    public async Task<int> AddAsync(CreateGrandPrixDto dto, CancellationToken ct)
    {
        var grandPrix = new GrandPrix
        {
            TrackLayoutId = dto.TrackLayoutId,
            SeasonId = dto.SeasonId,
            SeasonDivisionId = dto.SeasonDivisionId,
            Name = dto.Name,
            StartingAt = dto.StartingAt,
            VodUrl = dto.VodUrl,
            Slug = dto.Slug
        };

        racingContext.GrandPrix.Add(grandPrix);

        await racingContext.SaveChangesAsync(ct);

        return grandPrix.Id;
    }

    public async Task<int?> UpdateBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        string grandPrixSlug,
        UpdateGrandPrixDto dto,
        CancellationToken ct = default)
    {
        var entity = await racingContext.GrandPrix
            .InScope(leagueSlug, seasonSlug, divisionSlug)
            .FirstOrDefaultAsync(x => x.Slug == grandPrixSlug, ct);

        if (entity is null)
            return null;

        entity.TrackLayoutId = dto.TrackLayoutId;
        entity.Name = dto.Name;
        entity.StartingAt = dto.StartingAt;
        entity.VodUrl = dto.VodUrl;
        entity.Slug = dto.Slug;

        await racingContext.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> DeleteBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        string grandPrixSlug,
        CancellationToken ct = default)
    {
        return await racingContext.GrandPrix
            .InScope(leagueSlug, seasonSlug, divisionSlug)
            .Where(x => x.Slug == grandPrixSlug)
            .ExecuteDeleteAsync(ct);
    }
}
