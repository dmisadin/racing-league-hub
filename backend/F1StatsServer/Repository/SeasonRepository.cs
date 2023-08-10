using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Dto.TrackDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Repository
{
    public class SeasonRepository : GenericRepository<Season>, IGenericRepository<Season>, ISeasonRepository
    {
        private readonly AdventureContext _context;
        public SeasonRepository(AdventureContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SeasonDisplayDto> GetSeasonData(int id)
        {
            var query = _context.Set<Season>()
                                .AsSplitQuery()
                                .AsNoTracking()
                                .Where(season => season.Id == id)
                                .Select(season => new SeasonDisplayDto
                                {
                                    Name = season.Name,
                                    ImagePath = season.ImagePath,
                                    LapsRequiredPercentage = season.LapsRequiredPercentage,
                                    //Game = MyMapper<GameDto, Game>.Map(_context.Set<Game>().Where(game => game.Id == season.GameId).FirstOrDefault()),
                                    Game = MyMapper<GameDto, Game>.Map(season.Game),
                                    //Platform = MyMapper<PlatformDto, Platform>.Map(_context.Set<Platform>().Where(platform => platform.Id == season.PlatformId).FirstOrDefault()),
                                    Platform = MyMapper<PlatformDto, Platform>.Map(season.Platform),
                                    QualPoints = MyMapper<SeasonPointsDto, SeasonQualPoints>.MapList(season.SeasonQualPoints.ToList()),
                                    RacePoints = MyMapper<SeasonPointsDto, SeasonRacePoints>.MapList(season.SeasonRacePoints.ToList()),
                                    SprintPoints = MyMapper<SeasonPointsDto, SeasonSprintPoints>.MapList(season.SeasonSprintPoints.ToList()),
                                    LobbySettings = MyMapper<SeasonLobbySettingsDto, SeasonLobbySettings>.Map(season.SeasonLobbySetting),
                                    Assists = MyMapper<SeasonAssistsDto, SeasonAssists>.Map(season.SeasonAssist),
                                    FastestLapPoints = MyMapper<SeasonPointsDto, SeasonFastestLapPoints>.Map(season.SeasonFastestLapPoint),
                                    GrandPrixes = _context.Set<GrandPrix>()
                                                          .Where(grandPrix => grandPrix.SeasonId == id)
                                                          .Select(grandPrix => new GrandPrixSeasonDto
                                                          {
                                                              Id = grandPrix.Id,
                                                              Name = grandPrix.Name,
                                                              HasSprint = grandPrix.HasSprint,
                                                              YoutubeUrl = grandPrix.YoutubeUrl,
                                                              StartTime = grandPrix.StartTime,
                                                              Track = new TrackSeasonDto
                                                              {
                                                                  Id = grandPrix.Track.Id,
                                                                  Name = grandPrix.Track.Name,
                                                                  Location = grandPrix.Track.Location,
                                                                  ImagePath = grandPrix.Track.ImagePath,
                                                                  CountryIso = grandPrix.Track.Country.Iso,
                                                                  CountryName = grandPrix.Track.Country.NameEnglish
                                                              },
                                                              FastestDriverId = grandPrix.Races.Where(race => race.FastestLapInMs != null)
                                                                                       .OrderBy(race => race.FastestLapInMs)
                                                                                       .Select(race => race.DriverId).FirstOrDefault(),
                                                              Race = grandPrix.Races.Select(race => new ResultSeasonDto
                                                              {
                                                                  DriverId = race.DriverId,
                                                                  TeamId = race.TeamId,
                                                                  PointsGained = race.PointsGained,
                                                                  ResultStatus = race.ResultStatus
                                                              }).ToList(),
                                                              Qualifying = grandPrix.Qualifyings.Select(qualifying => new ResultSeasonDto
                                                              {
                                                                  DriverId = qualifying.DriverId,
                                                                  TeamId = qualifying.TeamId,
                                                                  PointsGained = qualifying.PointsGained,
                                                                  ResultStatus = qualifying.ResultStatus
                                                              }).ToList(),

                                                              Sprint = grandPrix.Sprints.Select(sprint => new ResultSeasonDto
                                                              {
                                                                  DriverId = sprint.DriverId,
                                                                  TeamId = sprint.TeamId,
                                                                  PointsGained = sprint.PointsGained,
                                                                  ResultStatus = sprint.ResultStatus
                                                              }).ToList(),
                                                          }).ToList(),

                                    Drivers = season.SeasonDrivers.Select(seasonDriver => new DriverSeasonDto
                                    {
                                        Id = seasonDriver.DriverId,
                                        Name = seasonDriver.Driver.Name,
                                        TeamId = seasonDriver.TeamId,
                                        CountryIso = seasonDriver.Driver.Country.Iso,
                                        PenaltyPoints = seasonDriver.PenaltyPoints
                                    }).ToList(),

                                    Teams = season.SeasonDrivers.Select(seasonDriver => new TeamDto
                                    {
                                        Id = seasonDriver.TeamId,
                                        Name = seasonDriver.Team.Name,
                                        ImagePath = seasonDriver.Team.ImagePath,
                                        ColorHex = seasonDriver.Team.ColorHex
                                    }).Distinct().ToList()

                                }).FirstOrDefaultAsync();

            return await query;
        }
    }
}
