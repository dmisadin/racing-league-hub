using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Entities;

namespace F1StatsServer.Interfaces
{
    public interface ISeasonRepository
    {
        Task<SeasonDisplayDto> GetSeasonData(int id);
        Task<SeasonSessionPointsDto> GetSeasonSessionPoints(int id);
        Task<SeasonAssistsDto?> GetSeasonAssists(int id);
        Task<SeasonLobbySettingsDto?> GetSeasonLobbySettings(int id);
        Task<IEnumerable<SeasonDriverDto>> GetSeasonDrivers(int seasonId);
    }
}
