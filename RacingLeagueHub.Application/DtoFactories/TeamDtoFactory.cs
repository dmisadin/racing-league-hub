using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Dtos.Team;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Storage;
using RacingLeagueHub.Domain.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoFactories;

public class TeamDtoFactory : DtoFactoryBase<Team, TeamDto>
{    
    public override bool FromDto(Team entity, TeamDto dto)
    {
        entity.Name = dto.Name;
        entity.Color = dto.Color;

        return true;
    }

    public override Expression<Func<Team, TeamDto>> ToDtoExpression()
    {
        var baseStorageUrl = S3Settings.Values.PublicBaseUrl;

        return team => new TeamDto
        {
            Id = new EncryptedId(team.Id),
            Name = team.Name,
            Color = team.Color,
            GameSpecificTeams = team.GameTeams
                .Select(gt => new GameTeamDto
                {
                    Id = new EncryptedId(gt.Id),
                    Game = gt.Game,
                    TeamId = new EncryptedId(gt.TeamId),
                    Name = gt.Name,
                    ShortName = gt.ShortName,
                    Abbreviation = gt.Abbreviation,
                    Color = gt.Color,
                    TelemetryId = gt.TelemetryId,
                    LogoResourceId = gt.LogoResourceId != null ? new EncryptedId(gt.LogoResourceId.Value) : null,
                    Logo = gt.LogoResourceId == null ? null : new ResourceBaseDto
                    {
                        Id = new EncryptedId(gt.LogoResource!.Id),
                        FileUrl = baseStorageUrl + "/uploads/" + gt.LogoResource.StorageId + "." + gt.LogoResource.Extension,
                        Extension = gt.LogoResource.Extension
                    }
                }).ToList()
        };
    }
}
