using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Domain.Leagues.Seasons;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Seasons.Mappers;

public class SeasonAssistsDtoMapper : DtoMapperBase<SeasonAssists, SeasonAssistsDto>
{
    public override bool FromDto(SeasonAssists entity, SeasonAssistsDto dto)
    {
        entity.SeasonId = dto.SeasonId;
        entity.RacingLine = dto.RacingLine;
        entity.Gearbox = dto.Gearbox;
        entity.TractionControl = dto.TractionControl;
        entity.Abs = dto.Abs;

        return true;
    }

    public override Expression<Func<SeasonAssists, SeasonAssistsDto>> ToDtoExpression()
    {
        return assists => new SeasonAssistsDto
        {
            Id = assists.Id,
            SeasonId = assists.SeasonId,
            RacingLine = assists.RacingLine,
            Gearbox = assists.Gearbox,
            TractionControl = assists.TractionControl,
            Abs = assists.Abs
        };
    }
}
