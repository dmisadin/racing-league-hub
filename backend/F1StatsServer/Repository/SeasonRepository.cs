using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.DriverDto;
using F1StatsServer.Dto.GrandPrixDto;
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
                                .Select(e => new SeasonDisplayDto
                                {
                                    Name = e.Name,
                                    ImagePath = e.ImagePath,
                                    LapsRequiredPercentage = e.LapsRequiredPercentage,
                                    Game = MyMapper<GameDto, Game>.Map(_context.Set<Game>().Where(d => d.Id == e.GameId).FirstOrDefault()),
                                    Platform = MyMapper<PlatformDto, Platform>.Map(_context.Set<Platform>().Where(d => d.Id == e.PlatformId).FirstOrDefault()),
                                    QualPoints = MyMapper<SeasonPointDto, SeasonQualPoint>.MapList(e.SeasonQualPoints.ToList()),
                                    RacePoints = MyMapper<SeasonPointDto, SeasonRacePoint>.MapList(e.SeasonRacePoints.ToList()),
                                    SprintPoints = MyMapper<SeasonPointDto, SeasonSprintPoint>.MapList(e.SeasonSprintPoints.ToList()),
                                    LobbySetting = MyMapper<SeasonLobbySettingDto, SeasonLobbySetting>.Map(e.SeasonLobbySetting),
                                    Assist = MyMapper<SeasonAssistDto, SeasonAssist>.Map(e.SeasonAssist),
                                    FastestLapPoint = MyMapper<SeasonPointDto, SeasonFastestLapPoint>.Map(e.SeasonFastestLapPoint),
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
                                                              Races = MyMapper<ResultSeasonDto, Race>.MapList(d.Races.ToList()),
                                                              Qualifications = MyMapper<ResultSeasonDto, Qualifying>.MapList(d.Qualifyings.ToList()),
                                                              Sprints = MyMapper<ResultSeasonDto, Sprint>.MapList(d.Sprints.ToList())

                                                          }).ToList(),
                                    Drivers = _context.Set<SeasonDriver>()
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
