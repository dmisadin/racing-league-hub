using RacingLeagueHub.Application.Dtos.Team;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoFactories;

public class GameTeamDtoFactory : DtoFactoryBase<GameTeam, GameTeamDto>
{
    public override bool FromDto(GameTeam entity, GameTeamDto dto)
    {
        entity.Game = dto.Game;
        entity.TeamId = dto.TeamId.RawId;
        entity.Name = dto.Name;
        entity.ShortName = dto.ShortName;
        entity.Abbreviation = dto.Abbreviation;
        entity.Color = dto.Color;
        entity.TelemetryId = dto.TelemetryId;
        entity.LogoResourceId = dto.LogoResourceId?.RawId;

        return true;
    }

    public override Expression<Func<GameTeam, GameTeamDto>> ToDtoExpression()
    {
        return gt => new GameTeamDto
        {
            Id = new EncryptedId(gt.Id),
            Game = gt.Game,
            TeamId = new EncryptedId(gt.TeamId),
            Name = gt.Name,
            ShortName = gt.ShortName,
            Abbreviation = gt.Abbreviation,
            Color = gt.Color,
            TelemetryId = gt.TelemetryId
        };
    }
}
