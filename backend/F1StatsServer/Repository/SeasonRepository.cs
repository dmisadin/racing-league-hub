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
                                                              YoutubeUrl = d.YoutubeUrl,
                                                              StartTime = d.StartTime,
                                                              Track = _context.Set<Track>()
                                                                              .Where(g => g.Id == d.TrackId)
                                                                              .Select(f => new TrackSeasonDto
                                                                              {
                                                                                  Id = f.Id,
                                                                                  Name = f.Name,
                                                                                  Location = f.Location,
                                                                                  ImagePath = f.ImagePath,
                                                                                  CountryIso = f.Country.Iso
                                                                              }).FirstOrDefault(),
                                                              FastestDriverId = d.Races.Where(g => g.FastestLapInMs != null)
                                                                                       .OrderBy(e => e.FastestLapInMs).Select(f => f.DriverId).FirstOrDefault(),
                                                              Race = _context.Set<Race>()
                                                                              .Where(g => g.GrandPrixId == d.Id)
                                                                              .Select(f => new ResultSeasonDto
                                                                              {
                                                                                  DriverId = f.DriverId,
                                                                                  TeamId = f.TeamId,
                                                                                  PointsGained = f.PointsGained
                                                                              }).ToList(),
                                                              Qualifying = _context.Set<Qualifying>()
                                                                              .Where(g => g.GrandPrixId == d.Id)
                                                                              .Select(f => new ResultSeasonDto
                                                                              {
                                                                                  DriverId = f.DriverId,
                                                                                  TeamId = f.TeamId,
                                                                                  PointsGained = f.PointsGained
                                                                              }).ToList(),

                                                              Sprint = _context.Set<Sprint>()
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
                                                          Id = d.DriverId,
                                                          Name = d.Driver.Name,
                                                          TeamId = d.TeamId,
                                                          CountryIso = d.Driver.Country.Iso,
                                                          PenaltyPoints = d.PenaltyPoints
                                                      }).ToList(),

                                    Teams = _context.Set<SeasonDrivers>()
                                                    .Select(d => new TeamDto
                                                    {
                                                        Id = d.TeamId,
                                                        Name = d.Team.Name,
                                                        ImagePath = d.Team.ImagePath,
                                                        ColorHex = d.Team.ColorHex
                                                    }).Distinct().ToList()
                                });

            return query;
        }
    }
}
