using F1StatsServer.Dto;

namespace F1StatsServer.Interface
{
    public interface ILeagueService
    {
        bool InsertLeague(LeagueInsertDto data);
    }
}
