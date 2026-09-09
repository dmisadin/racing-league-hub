using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Domain.Entities.GrandsPrix;

namespace RacingLeagueHub.Infrastructure.Persistence.GrandsPrix;

internal class GrandPrixCommands : IGrandPrixCommands
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<GrandPrix, GrandPrixDto> mapper;

    public GrandPrixCommands(RacingContext racingContext,
        IDtoMapper<GrandPrix, GrandPrixDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<GrandPrixDto> AddAsync(CreateGrandPrixDto dto, CancellationToken ct)
    {
        var grandPrix = new GrandPrix
        {
            TrackLayoutId = dto.TrackLayoutId.RawId,
            SeasonId = dto.SeasonId.RawId,
            Name = dto.Name,
            StartingAt = dto.StartingAt,
            VodUrl = dto.VodUrl,
            Slug = dto.Slug
        };

        racingContext.GrandPrix.Add(grandPrix);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(grandPrix);
    }

    public async Task<long?> UpdateBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        UpdateGrandPrixDto dto,
        CancellationToken ct = default)
    {
        var entity = await racingContext.GrandPrix
            .FirstOrDefaultAsync(x =>
                x.Slug == grandPrixSlug
                && x.Season.Slug == seasonSlug
                && x.Season.League.Slug == leagueSlug,
                ct);

        if (entity is null)
            return null;

        entity.TrackLayoutId = dto.TrackLayoutId.RawId;
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
        string grandPrixSlug,
        CancellationToken ct = default)
    {
        return await racingContext.GrandPrix
            .Where(x =>
                x.Slug == grandPrixSlug 
                && x.Season.Slug == seasonSlug 
                && x.Season.League.Slug == leagueSlug)
            .ExecuteDeleteAsync(ct);
    }
}
