using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Entities.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons;

internal class SeasonCommands : ISeasonCommands
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Season, SeasonDto> mapper;

    public SeasonCommands(RacingContext racingContext,
        IDtoMapper<Season, SeasonDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<SeasonDto> AddAsync(CreateSeasonDto dto, CancellationToken ct = default)
    {
        var season = new Season
        {
            LeagueId = dto.LeagueId.RawId,
            Name = dto.Name,
            Platform = dto.Platform,
            Game = dto.Game,
            LapPercentageRequired = dto.LapPercentageRequired,
            Slug = dto.Slug,
            LogoResourceId = dto.LogoResourceId?.RawId
        };

        racingContext.Season.Add(season);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(season);
    }

    public async Task<long?> UpdateBySlugAsync(string leagueSlug, string seasonSlug, UpdateSeasonDto dto, CancellationToken ct = default)
    {
        var entity = await racingContext.Season
            .FirstOrDefaultAsync(x =>
                x.Slug == seasonSlug 
                && x.League.Slug == leagueSlug, ct);

        if (entity is null)
            return null;

        entity.Name = dto.Name;
        entity.Platform = dto.Platform;
        entity.Game = dto.Game;
        entity.LapPercentageRequired = dto.LapPercentageRequired;
        entity.Slug = dto.Slug;
        entity.LogoResourceId = dto.LogoResourceId?.RawId;

        await racingContext.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> DeleteBySlugAsync(string leagueSlug, string seasonSlug, CancellationToken ct = default)
    {
        return await racingContext.Season
            .Where(x =>
                x.Slug == seasonSlug
                && x.League.Slug == leagueSlug)
            .ExecuteDeleteAsync(ct);
    }
}
