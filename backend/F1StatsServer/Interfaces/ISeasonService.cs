using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Interfaces
{
    public interface ISeasonService
    {
        public Task<int> InsertSeasonAsync(SeasonInsertDto data);
        public Task<SeasonDisplayDto> GetSeasonDataAsync(int id);
        public Task<SeasonSessionPointsDto> GetSeasonSessionPointsAsync(int id);
        public Task<SeasonAssistsDto?> GetAssistsAsync(int id);
        public Task<SeasonLobbySettingsDto?> GetLobbySettingsAsync(int id);
        public Task<IEnumerable<SeasonDriverDto>> GetSeasonDrivers(int seasonId);
        //public Task<SeasonTeams> GetSeasonTeams(int seasonId);
    }
}
