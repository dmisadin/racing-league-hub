using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.GameTeams.Dtos;
using RacingLeagueHub.Application.Resources;
using RacingLeagueHub.Application.Teams.Dtos;
using RacingLeagueHub.Domain.Teams;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Teams.Mappers;

public class TeamDtoMapper(IStorageService storageService) 
    : DtoMapperBase<Team, TeamDto>
{    
    public override bool FromDto(Team entity, TeamDto dto)
    {
        entity.Name = dto.Name;
        entity.Color = dto.Color;

        return true;
    }

    public override Expression<Func<Team, TeamDto>> ToDtoExpression()
    {
        var baseStorageUrl = storageService.GetBaseUrl();

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
                    LogoUrl = gt.LogoResourceId == null 
                        ? null 
                        : baseStorageUrl + "/uploads/" + gt.LogoResource.StorageId + "." + gt.LogoResource.Extension
                }).ToList()
        };
    }
}
