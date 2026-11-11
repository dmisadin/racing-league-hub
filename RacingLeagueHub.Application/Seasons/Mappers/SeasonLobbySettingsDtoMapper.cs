using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Domain.Leagues.Seasons;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Seasons.Mappers;

public class SeasonLobbySettingsDtoMapper : DtoMapperBase<SeasonLobbySettings, SeasonLobbySettingsDto>
{
    public override bool FromDto(SeasonLobbySettings entity, SeasonLobbySettingsDto dto)
    {
        entity.SeasonId = dto.SeasonId;
        entity.QualifyingType = dto.QualifyingType;
        entity.RaceDistancePercentage = dto.RaceDistancePercentage;
        entity.FormationLap = dto.FormationLap;
        entity.Weather = dto.Weather;
        entity.CornerCutting = dto.CornerCutting;
        entity.CarDamage = dto.CarDamage;
        entity.CarDamageRate = dto.CarDamageRate;
        entity.ParcFerme = dto.ParcFerme;
        entity.EqualCarPerformance = dto.EqualCarPerformance;
        entity.SafetyCar = dto.SafetyCar;
        entity.Collisions = dto.Collisions;
        entity.Ghosting = dto.Ghosting;
        entity.RaceStart = dto.RaceStart;

        return true;
    }

    public override Expression<Func<SeasonLobbySettings, SeasonLobbySettingsDto>> ToDtoExpression()
    {
        return lobby => new SeasonLobbySettingsDto
        {
            Id = lobby.Id,
            SeasonId = lobby.SeasonId,
            QualifyingType = lobby.QualifyingType,
            RaceDistancePercentage = lobby.RaceDistancePercentage,
            FormationLap = lobby.FormationLap,
            Weather = lobby.Weather,
            CornerCutting = lobby.CornerCutting,
            CarDamage = lobby.CarDamage,
            CarDamageRate = lobby.CarDamageRate,
            ParcFerme = lobby.ParcFerme,
            EqualCarPerformance = lobby.EqualCarPerformance,
            SafetyCar = lobby.SafetyCar,
            Collisions = lobby.Collisions,
            Ghosting = lobby.Ghosting,
            RaceStart = lobby.RaceStart
        };
    }
}
