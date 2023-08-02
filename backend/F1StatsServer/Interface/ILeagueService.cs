using F1StatsServer.Dto.LeagueDtos;

namespace F1StatsServer.Interface
{
    public interface ILeagueService
    {
        int InsertLeague(LeagueInsertDto data);
        LeagueDisplayDto GetLeagueData(int id);
    }
}
