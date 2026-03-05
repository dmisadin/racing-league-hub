using RacingLeagueHub.BLL.Models.Dtos.Team;
using RacingLeagueHub.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories;

public class TeamDtoFactory : DtoFactoryBase<Team, TeamDto>
{    
    public override void FromDto(Team entity, TeamDto dto)
    {
        entity.Name = dto.Name;
        entity.Color = dto.Color;
    }

    public override Expression<Func<Team, TeamDto>> ToDtoExpression()
    {
        return team => new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            Color = team.Color,
            GameSpecificTeams = team.GameTeams
                .Select(gt => new GameTeamDto
                {
                    Id = gt.Id,
                    TeamId = gt.TeamId,
                    DisplayName = gt.DisplayName,
                    Color = gt.Color,
                    TelemetryId = gt.TelemetryId
                }).ToList()
        };
    }
}
