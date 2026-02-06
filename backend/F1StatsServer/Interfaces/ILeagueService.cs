using F1StatsServer.Dto.LeagueDtos;

namespace F1StatsServer.Interfaces
{
    public interface ILeagueService
    {
        Task<int> InsertLeagueAsync(LeagueInsertDto data);
        Task<LeagueDisplayDto> GetLeagueDataAsync(int id);
        Task<List<LeaguesDisplayDto>> GetLeaguesAsync();
    }
}
