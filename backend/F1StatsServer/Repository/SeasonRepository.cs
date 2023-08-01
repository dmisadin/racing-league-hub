using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.DriverDto;
using F1StatsServer.Dto.GrandPrixDto;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Dto.SeasonDtos;
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

        public IQueryable<SeasonDisplayDto> GetSeasonData(int id)
        {
            var query = _context.Set<Season>()
                                .AsSplitQuery()
                                .Where(c => c.Id == id)
                                .Select(b => new SeasonDisplayDto
                                {
                                    Name = b.Name,
                                    ImagePath = b.ImagePath,
                                    LapsRequiredPercentage = b.LapsRequiredPercentage,
                                    Game = MyMapper<GameDto, Game>.Map(_context.Set<Game>().Where(d => d.Id == b.GameId).FirstOrDefault()),
                                    Platform = MyMapper<PlatformDto, Platform>.Map(_context.Set<Platform>().Where(d => d.Id == b.PlatformId).FirstOrDefault()),
                                    QualPoints = MyMapper<SeasonPointsDto, SeasonQualPoints>.MapList(b.SeasonQualPoints.ToList()),
                                    RacePoints = MyMapper<SeasonPointsDto, SeasonRacePoints>.MapList(b.SeasonRacePoints.ToList()),
                                    SprintPoints = MyMapper<SeasonPointsDto, SeasonSprintPoints>.MapList(b.SeasonSprintPoints.ToList()),
                                    LobbySettings = MyMapper<SeasonLobbySettingsDto, SeasonLobbySettings>.Map(b.SeasonLobbySetting),
                                    Assists = MyMapper<SeasonAssistsDto, SeasonAssists>.Map(b.SeasonAssist),
                                    FastestLapPoints = MyMapper<SeasonPointsDto, SeasonFastestLapPoints>.Map(b.SeasonFastestLapPoint),
                                    GrandPrixes = _context.Set<GrandPrix>()
                                                          .Where(d => d.SeasonId == id)
                                                          .Select(d => new GrandPrixSeasonDto
                                                          {
                                                              Name = d.Name,
                                                              HasSprint = d.HasSprint,
                                                              YoutubeUrl = d.YouTubeUrl,
                                                              Laps = d.Track.Laps,
                                                              CountryIso = d.Track.Country.Iso,
                                                              FastestDriverId = d.Races.Where(g => g.FastestLapInMs != null)
                                                                                       .OrderBy(e => e.FastestLapInMs).Select(f => f.DriverId).FirstOrDefault(),
                                                              Races = _context.Set<Race>()
                                                                              .Where(g => g.GrandPrixId == d.Id)
                                                                              .Select(f => new ResultSeasonDto
                                                                              {
                                                                                  DriverId = f.DriverId,
                                                                                  TeamId = f.TeamId,
                                                                                  PointsGained = f.PointsGained
                                                                              }).ToList(),
                                                              Qualifications = _context.Set<Qualifying>()
                                                                              .Where(g => g.GrandPrixId == d.Id)
                                                                              .Select(f => new ResultSeasonDto
                                                                              {
                                                                                  DriverId = f.DriverId,
                                                                                  TeamId = f.TeamId,
                                                                                  PointsGained = f.PointsGained
                                                                              }).ToList(),

                                                              Sprints = _context.Set<Sprint>()
                                                                              .Where(g => g.GrandPrixId == d.Id)
                                                                              .Select(f => new ResultSeasonDto
                                                                              {
                                                                                  DriverId = f.DriverId,
                                                                                  TeamId = f.TeamId,
                                                                                  PointsGained = f.PointsGained
                                                                              }).ToList(),
                                                          }).ToList(),
                                    Drivers = _context.Set<SeasonDrivers>()
                                                      .Where(d => d.SeasonId == id)
                                                      .Select(d => new DriverSeasonDto
                                                      {
                                                          Name = d.Driver.Name,
                                                          TeamName = d.Team.Name,
                                                          TeamColorHex = d.Team.ColorHex,
                                                          TeamImagePath = d.Team.ImagePath,
                                                          CountryIso = d.Driver.Country.Iso,
                                                          PenaltyPoints = d.PenaltyPoints
                                                      }).ToList(),
                                });

            return query;
        }
    }
}
