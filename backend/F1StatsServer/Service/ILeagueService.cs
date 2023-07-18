using F1StatsServer.Dto;

namespace F1StatsServer.Service
{
    public interface ILeagueService
    {
        bool InsertLeague(LeagueInsertDto data);
    }
}
