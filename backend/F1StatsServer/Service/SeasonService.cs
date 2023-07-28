using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;

namespace F1StatsServer.Service
{
    public class SeasonService : ISeasonService
    {
        private readonly IGenericRepository<Season> _genericRepository;

        public SeasonService(IGenericRepository<Season> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int InsertSeason(SeasonInsertDto data)
        {
            var item = new Season
            {
                LeagueId = data.LeagueId,
                GameId = data.GameId,
                PlatformId = data.PlatformId,
                Name = data.Name,
                ImagePath = data.ImagePath,
                LapsRequiredPercentage = data.LapsRequiredPercentage,
                SeasonRacePoints = MyMapper<SeasonRacePoint, SeasonPointDto>.MapList(data.RacePointsDto),
                SeasonLobbySetting = MyMapper<SeasonLobbySetting, SeasonLobbySettingDto>.Map(data.LobbySettingDto),
                SeasonAssist = MyMapper<SeasonAssist, SeasonAssistDto>.Map(data.AssistDto),
                SeasonQualPoints = MyMapper<SeasonQualPoint, SeasonPointDto>.MapList(data.QualPointsDto),
                SeasonSprintPoints = MyMapper<SeasonSprintPoint, SeasonPointDto>.MapList(data.SprintPointsDto),
                SeasonFastestLapPoint = MyMapper<SeasonFastestLapPoint, SeasonPointDto>.Map(data.FastestLapPointDto)
            };

            if (item == null)
                return -1;

            return _genericRepository.CreateItem(item);
        }
    }
}
