using F1StatsServer.Dto.LeagueDtos;

namespace F1StatsServer.Interface
{
    public interface ILeagueRepository
    {
        public Task<LeagueDisplayDto> GetLeagueDataAsync(int id);
        public Task<List<LeaguesDisplayDto>> GetLeaguesAsync();
    }
}
