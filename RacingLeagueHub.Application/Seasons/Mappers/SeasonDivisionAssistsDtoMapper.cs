using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Domain.Leagues.Seasons;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Seasons.Mappers;

public class SeasonDivisionAssistsDtoMapper : DtoMapperBase<SeasonDivisionAssists, SeasonDivisionAssistsDto>
{
    public override bool FromDto(SeasonDivisionAssists entity, SeasonDivisionAssistsDto dto)
    {
        entity.SeasonDivisionId = dto.SeasonDivisionId;
        entity.RacingLine = dto.RacingLine;
        entity.Gearbox = dto.Gearbox;
        entity.TractionControl = dto.TractionControl;
        entity.Abs = dto.Abs;

        return true;
    }

    public override Expression<Func<SeasonDivisionAssists, SeasonDivisionAssistsDto>> ToDtoExpression()
    {
        return assists => new SeasonDivisionAssistsDto
        {
            Id = assists.Id,
            SeasonDivisionId = assists.SeasonDivisionId,
            RacingLine = assists.RacingLine,
            Gearbox = assists.Gearbox,
            TractionControl = assists.TractionControl,
            Abs = assists.Abs
        };
    }
}
