using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Leagues.Dtos;
using RacingLeagueHub.Application.Leagues.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Leagues;

internal class LeagueCommands : ILeagueCommands
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<League, LeagueDto> mapper;

    public LeagueCommands(RacingContext racingContext,
        IDtoMapper<League, LeagueDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<LeagueDto> AddAsync(CreateLeagueDto dto, CancellationToken ct = default)
    {
        var league = new League
        {
            Region = dto.Region,
            Name = dto.Name,
            Abbreviation = dto.Abbreviation,
            Description = dto.Description,
            Timezone = dto.Timezone,
            Slug = dto.Slug,
            LogoResourceId = dto.LogoResourceId?.RawId
        };

        racingContext.League.Add(league);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(league);
    }

    public async Task<long?> UpdateBySlugAsync(string leagueSlug, UpdateLeagueDto dto, CancellationToken ct = default)
    {
        var entity = await racingContext.League
            .FirstOrDefaultAsync(x => x.Slug == leagueSlug, ct);

        if (entity is null)
            return null;

        entity.Region = dto.Region;
        entity.Name = dto.Name;
        entity.Abbreviation = dto.Abbreviation;
        entity.Description = dto.Description;
        entity.Timezone = dto.Timezone;
        entity.Slug = dto.Slug;
        entity.LogoResourceId = dto.LogoResourceId?.RawId;

        await racingContext.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> DeleteBySlugAsync(string leagueSlug, CancellationToken ct = default)
    {
        return await racingContext.League
            .Where(x => x.Slug == leagueSlug)
            .ExecuteDeleteAsync(ct);
    }
}
