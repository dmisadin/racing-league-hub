using RacingLeagueHub.BLL.Models.Dtos.Team;
using RacingLeagueHub.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories;

public class GameTeamDtoFactory : DtoFactoryBase<GameTeam, GameTeamDto>
{
    public override void FromDto(GameTeam entity, GameTeamDto dto)
    {
        entity.Game = dto.Game;
        entity.TeamId = dto.TeamId;
        entity.DisplayName = dto.DisplayName;
        entity.Color = dto.Color;
        entity.TelemetryId = dto.TelemetryId;
    }

    public override Expression<Func<GameTeam, GameTeamDto>> ToDtoExpression()
    {
        return gt => new GameTeamDto
        {
            Id = gt.Id,
            TeamId = gt.TeamId,
            DisplayName = gt.DisplayName,
            Color = gt.Color,
            TelemetryId = gt.TelemetryId
        };
    }
}
