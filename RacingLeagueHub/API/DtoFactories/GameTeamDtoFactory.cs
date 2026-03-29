using RacingLeagueHub.API.Dtos.Team;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Models;
using System.Linq.Expressions;

namespace RacingLeagueHub.API.DtoFactories;

public class GameTeamDtoFactory : DtoFactoryBase<GameTeam, GameTeamDto>
{
    public override void FromDto(GameTeam entity, GameTeamDto dto)
    {
        entity.Game = dto.Game;
        entity.TeamId = dto.TeamId.RawId;
        entity.Name = dto.Name;
        entity.ShortName = dto.ShortName;
        entity.Abbreviation = dto.Abbreviation;
        entity.Color = dto.Color;
        entity.TelemetryId = dto.TelemetryId;
        entity.LogoResourceId = dto.LogoResourceId?.RawId;
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
