using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Models.Dtos.Team;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories;

public class GameTeamDtoFactory : DtoFactoryBase<GameTeam, GameTeamDto>
{
    public override void FromDto(GameTeam entity, GameTeamDto dto)
    {
        entity.Game = dto.Game;
        entity.TeamId = dto.TeamId;
        entity.Name = dto.Name;
        entity.ShortName = dto.ShortName;
        entity.Abbreviation = dto.Abbreviation;
        entity.Color = dto.Color;
        entity.TelemetryId = dto.TelemetryId;
    }

    public override Expression<Func<GameTeam, GameTeamDto>> ToDtoExpression()
    {
        return gt => new GameTeamDto
        {
            Id = gt.Id,
            Game = gt.Game,
            TeamId = gt.TeamId,
            Name = gt.Name,
            ShortName = gt.ShortName,
            Abbreviation = gt.Abbreviation,
            Color = gt.Color,
            TelemetryId = gt.TelemetryId
        };
    }
}
