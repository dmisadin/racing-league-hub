using F1StatsServer.Dto.LeagueDto;

namespace F1StatsServer.Interface
{
    public interface ILeagueService
    {
        int InsertLeague(LeagueInsertDto data);
    }
}
