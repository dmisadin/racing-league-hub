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

        public bool InsertSeason(SeasonInsertDto data)
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
                SeasonLobbySettings = MyMapper<SeasonLobbySetting, SeasonLobbySettingDto>.MapList(data.LobbySettingDto),
                SeasonAssists = MyMapper<SeasonAssist, SeasonAssistDto>.MapList(data.AssistDto),
                SeasonQualPoints = MyMapper<SeasonQualPoint, SeasonPointDto>.MapList(data.QualPointsDto),
                SeasonSprintPoints = MyMapper<SeasonSprintPoint, SeasonPointDto>.MapList(data.SprintPointsDto),
                SeasonFastestLapPoints = MyMapper<SeasonFastestLapPoint, SeasonFastestLapPointDto>.MapList(data.FastestLapPointDto)
            };

            if (item == null)
                return false;

            return _genericRepository.CreateItem(item);
        }
    }
}
