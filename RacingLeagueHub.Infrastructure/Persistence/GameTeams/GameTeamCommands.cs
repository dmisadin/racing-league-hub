using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Services.GameTeamService.Dtos;
using RacingLeagueHub.Application.Services.GameTeamService.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.GameTeams;

internal class GameTeamCommands : IGameTeamCommands
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<GameTeam, GameTeamDto> mapper;

    public GameTeamCommands(
        RacingContext racingContext,
        IDtoMapper<GameTeam, GameTeamDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<GameTeamDto> AddAsync(CreateGameTeamDto dto, CancellationToken ct)
    {
        var gameTeam = new GameTeam
        {
            Game = dto.Game,
            TeamId = dto.TeamId.RawId,
            Name = dto.Name,
            ShortName = dto.ShortName,
            Abbreviation = dto.Abbreviation,
            Color = dto.Color,
            TelemetryId = dto.TelemetryId,
            LogoResourceId = dto.LogoResourceId?.RawId
        };

        racingContext.GameTeam.Add(gameTeam);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(gameTeam);
    }

    public async Task<GameTeamDto?> UpdateAsync(long id, UpdateGameTeamDto dto, CancellationToken ct)
    {
        var gameTeam = await racingContext.GameTeam
            .SingleOrDefaultAsync(t => t.Id == id, ct);

        if (gameTeam is null)
            return null;

        gameTeam.Game = dto.Game;
        gameTeam.Name = dto.Name;
        gameTeam.ShortName = dto.ShortName;
        gameTeam.Abbreviation = dto.Abbreviation;
        gameTeam.Color = dto.Color;
        gameTeam.TelemetryId = dto.TelemetryId;
        gameTeam.LogoResourceId = dto.LogoResourceId?.RawId;

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(gameTeam);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var gameTeam = await racingContext.GameTeam
            .SingleOrDefaultAsync(t => t.Id == id, ct);

        if (gameTeam is null)
            return false;

        racingContext.GameTeam.Remove(gameTeam);

        await racingContext.SaveChangesAsync(ct);

        return true;
    }
}
