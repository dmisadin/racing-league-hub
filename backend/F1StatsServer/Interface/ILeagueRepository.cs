using F1StatsServer.Dto.LeagueDto;

namespace F1StatsServer.Interface
{
    public interface ILeagueRepository
    {
        public IQueryable<LeagueDisplayDto> GetLeagueData(int id);
    }
}
