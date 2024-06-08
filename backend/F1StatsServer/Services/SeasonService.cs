using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;
using F1StatsServer.Utility;
using F1StatsServer.Entities.Enums;

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


        public async Task<int> InsertSeasonAsync(SeasonInsertDto season)
        {
            var item = new Season
            {
                LeagueId = season.LeagueId,
                Game = season.Game,
                Platform = season.Platform,
                Name = season.Name,
                ImagePath = season.ImagePath,
                LapsRequiredPercentage = season.LapsRequiredPercentage,
                SeasonPoints = MyMapper<SeasonPoints, SeasonPointsDto>.MapList(season.SeasonPoints),
                SeasonLobbySetting = MyMapper<SeasonLobbySettings, SeasonLobbySettingsDto>.Map(season.LobbySettings),
                SeasonAssist = MyMapper<SeasonAssists, SeasonAssistsDto>.Map(season.Assists),
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

        public async Task<IEnumerable<SeasonDriverDto>> GetSeasonDrivers(int seasonId)
        {
            if (!_genericRepository.Has(seasonId))
                return null;

            return await _seasonRepository.GetSeasonDrivers(seasonId);
        }
    }
}
