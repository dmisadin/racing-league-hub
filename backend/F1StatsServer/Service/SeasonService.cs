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
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(IGenericRepository<Season> genericRepository, ISeasonRepository seasonRepository)
        {
            _genericRepository = genericRepository;
            _seasonRepository = seasonRepository;
        }

        public async Task<SeasonDisplayDto> GetSeasonData(int id)
        {
            if (!_genericRepository.Has(id))
                return null;

            var item = await _seasonRepository.GetSeasonData(id);

            if (item == null)
                return null;

            return item;

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
                SeasonRacePoints = MyMapper<SeasonRacePoints, SeasonPointsDto>.MapList(data.RacePointsDto),
                SeasonLobbySetting = MyMapper<SeasonLobbySettings, SeasonLobbySettingsDto>.Map(data.LobbySettingsDto),
                SeasonAssist = MyMapper<SeasonAssists, SeasonAssistsDto>.Map(data.AssistsDto),
                SeasonQualPoints = MyMapper<SeasonQualPoints, SeasonPointsDto>.MapList(data.QualPointsDto),
                SeasonSprintPoints = MyMapper<SeasonSprintPoints, SeasonPointsDto>.MapList(data.SprintPointsDto),
                SeasonFastestLapPoint = MyMapper<SeasonFastestLapPoints, SeasonPointsDto>.Map(data.FastestLapPointDto)
            };

            if (item == null)
                return -1;

            return _genericRepository.CreateItem(item);
        }
    }
}
