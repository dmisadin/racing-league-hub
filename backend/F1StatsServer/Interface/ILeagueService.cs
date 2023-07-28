using F1StatsServer.Dto.LeagueDto;

namespace F1StatsServer.Interface
{
    public interface ILeagueService
    {
        bool InsertLeague(LeagueInsertDto data);
        LeagueDisplayDto GetLeagueData(int id);
    }
}
