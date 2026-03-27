using RacingLeagueHub.API.Dtos.Resource;
using RacingLeagueHub.API.Dtos.Team;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Models.Storage;
using System.Linq.Expressions;

namespace RacingLeagueHub.API.DtoFactories;

public class TeamDtoFactory : DtoFactoryBase<Team, TeamDto>
{    
    public override void FromDto(Team entity, TeamDto dto)
    {
        entity.Name = dto.Name;
        entity.Color = dto.Color;
    }

    public override Expression<Func<Team, TeamDto>> ToDtoExpression()
    {
        var baseStorageUrl = S3Settings.Values.PublicBaseUrl;

        return team => new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            Color = team.Color,
            GameSpecificTeams = team.GameTeams
                .Select(gt => new GameTeamDto
                {
                    Id = gt.Id,
                    Game = gt.Game,
                    TeamId = gt.TeamId,
                    Name = gt.Name,
                    ShortName = gt.ShortName,
                    Abbreviation = gt.Abbreviation,
                    Color = gt.Color,
                    TelemetryId = gt.TelemetryId,
                    LogoResourceId = gt.LogoResourceId,
                    Logo = gt.LogoResourceId == null ? null : new ResourceBaseDto
                    {
                        Id = gt.LogoResource!.Id,
                        FileUrl = baseStorageUrl + "/uploads/" + gt.LogoResource.StorageId + "." + gt.LogoResource.Extension,
                        Extension = gt.LogoResource.Extension
                    }
                }).ToList()
        };
    }
}
