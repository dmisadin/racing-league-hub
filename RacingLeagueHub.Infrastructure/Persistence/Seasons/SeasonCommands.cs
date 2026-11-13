using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Leagues.Divisions;
using RacingLeagueHub.Domain.Leagues.Seasons;

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
        var divisions = await racingContext.Division
            .Where(d => d.LeagueId == dto.LeagueId)
            .ToListAsync(ct);

        if (divisions.Count == 0)
        {
            var division = Division.CreateDefault();
            division.LeagueId = dto.LeagueId;
            divisions.Add(division);
        }

        var season = new Season
        {
            LeagueId = dto.LeagueId,
            Name = dto.Name,
            Platform = dto.Platform,
            Game = dto.Game,
            LapPercentageRequired = dto.LapPercentageRequired,
            Slug = dto.Slug,
            LogoResourceId = dto.LogoResourceId,
            SeasonDivisions = divisions
                .Select(d => new SeasonDivision { Division = d })
                .ToList()
        };

        racingContext.Season.Add(season);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(season);
    }

    public async Task<int?> UpdateBySlugAsync(string leagueSlug, string seasonSlug, UpdateSeasonDto dto, CancellationToken ct = default)
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
        entity.LogoResourceId = dto.LogoResourceId;

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
