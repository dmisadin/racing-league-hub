using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;
using F1StatsServer.Utility;

namespace F1StatsServer.Services
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

        public async Task<SeasonDisplayDto> GetSeasonDataAsync(int id)
        {
            if (!_genericRepository.Has(id))
                return null;

            var item = await _seasonRepository.GetSeasonData(id);

            return item;

        }

        public async Task<SeasonSessionPointsDto> GetSeasonSessionPointsAsync(int id)
        {
            if (!_genericRepository.Has(id))
                return null;

            var item = await _seasonRepository.GetSeasonSessionPoints(id);

            return item;

        }


        public async Task<int> InsertSeasonAsync(SeasonInsertDto data)
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

            return await _genericRepository.CreateItemAsync(item);
        }

        public async Task<SeasonAssistsDto?> GetAssistsAsync(int id)
        {
            if (!_genericRepository.Has(id))
                return null;

            return await _seasonRepository.GetSeasonAssists(id);
        }
        public async Task<SeasonLobbySettingsDto?> GetLobbySettingsAsync(int id)
        {
            if (!_genericRepository.Has(id))
                return null;

            return await _seasonRepository.GetSeasonLobbySettings(id);
        }
    }
}
