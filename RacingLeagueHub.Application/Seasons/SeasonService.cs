using RacingLeagueHub.Application.Leagues.Persistence;
using RacingLeagueHub.Application.Seasons.Persistence;

namespace RacingLeagueHub.Application.Seasons;

internal class SeasonService : ISeasonService
{
    private readonly ISeasonQueries seasonQueries;
    private readonly ISeasonCommands seasonCommands;
    private readonly ILeagueQueries leagueQueries;

    public SeasonService(
        ISeasonQueries seasonQueries,
        ISeasonCommands seasonCommands, 
        ILeagueQueries leagueQueries)
    {
        this.seasonQueries = seasonQueries;
        this.seasonCommands = seasonCommands;
        this.leagueQueries = leagueQueries;
    }

}
