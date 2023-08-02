using F1StatsServer.Dto.LeagueDtos;

namespace F1StatsServer.Interface
{
    public interface ILeagueRepository
    {
        public IQueryable<LeagueDisplayDto> GetLeagueData(int id);
        public List<LeaguesDisplayDto> GetLeagues();
    }
}
