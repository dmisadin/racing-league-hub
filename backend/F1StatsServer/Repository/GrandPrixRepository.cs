using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Dto.TrackDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Repository
{
    public class GrandPrixRepository : GenericRepository<GrandPrix>, IGenericRepository<GrandPrix>, IGrandPrixRepository
    {
        private readonly AdventureContext _context;
        public GrandPrixRepository(AdventureContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GrandPrixHomeDto>> GetDataAsync()
        {
            var query = (from GrandPrix in _context.Set<GrandPrix>()
                        join Season in _context.Set<Season>() on GrandPrix.SeasonId equals Season.Id
                        join League in _context.Set<League>() on Season.LeagueId equals League.Id
                        join Track in _context.Set<Track>() on GrandPrix.TrackId equals Track.Id
                        orderby GrandPrix.StartTime descending
                        select new GrandPrixHomeDto
                        {
                            Id = GrandPrix.Id,
                            Name = GrandPrix.Name,
                            StartTime = GrandPrix.StartTime,
                            YoutubeUrl = GrandPrix.YoutubeUrl,
                            Season = new Dto.SeasonDtos.SeasonHomeDto 
                            { 
                                Id = Season.Id,
                                Name = Season.Name,
                            },
                            League = new Dto.LeagueDtos.LeagueHomeDto
                            {
                                Id = League.Id,
                                Name = League.Name
                            },
                            Track = new Dto.TrackDtos.TrackHomeDto
                            {
                                Id = Track.Id,
                                Name = Track.Name,
                                Location = Track.Location
                            }
                        }).Take(5);
            var result = await query.ToListAsync();

            return result;
        }

        public async Task<GrandPrixDisplayDto?> GetGrandPrixDataAsync(int id)
        {
            var query = _context.Set<GrandPrix>()
                                .AsSplitQuery()
                                .AsNoTracking()
                                .Where(grandPrix => grandPrix.Id == id)
                                .Select(grandPrix => new GrandPrixDisplayDto
                                {
                                    Name = grandPrix.Name,
                                    StartTime = grandPrix.StartTime,
                                    YoutubeUrl = grandPrix.YoutubeUrl,
                                    FastestDriverId = grandPrix.Races.Where(race => race.FastestLapInMs != null)
                                                                    .OrderBy(race => race.FastestLapInMs)
                                                                    .Select(race => race.DriverId).FirstOrDefault(),
                                    Track = new TrackDto
                                    {
                                        Id = grandPrix.Track.Id,
                                        Name = grandPrix.Track.Name,
                                        Location = grandPrix.Track.Location,
                                        CornersTotal = grandPrix.Track.CornersTotal,
                                        CornersLeft = grandPrix.Track.CornersLeft,
                                        Elevation = grandPrix.Track.Elevation,
                                        Length = grandPrix.Track.Length,
                                        PitStop = grandPrix.Track.PitStop,
                                        ImagePath = grandPrix.Track.ImagePath,
                                        Laps = grandPrix.Track.Laps,
                                        Country = new CountryDto
                                        {
                                            Id = grandPrix.Track.Country.Id,
                                            NameCroatian = grandPrix.Track.Country.NameCroatian,
                                            NameEnglish = grandPrix.Track.Country.NameEnglish,
                                            Iso = grandPrix.Track.Country.Iso,
                                            Iso3 = grandPrix.Track.Country.Iso3,
                                            ImagePath = grandPrix.Track.Country.ImagePath
                                        }

                                    },

                                    Race = grandPrix.Races.Select(race => new RaceDto
                                    {
                                        DriverId = race.DriverId,
                                        TeamId = race.TeamId,
                                        PointsGained = race.PointsGained,
                                        Id = race.Id,
                                        Position = race.Position,
                                        IsReserve = race.IsReserve,
                                        RaceTime = race.RaceTime,
                                        TimePenalty = race.TimePenalty,
                                        LapsCompleted = race.LapsCompleted,
                                        GridPosition = race.GridPosition,
                                        UsedTyres = race.UsedTyres,
                                        FastestLapInMs = race.FastestLapInMs,
                                        ResultStatus = race.ResultStatus,
                                        PostRaceTimePenalty = race.PostRaceTimePenalty
                                    }).ToList(),

                                    Qualifying = grandPrix.Qualifyings.Select(qualifying => new QualDto
                                    {
                                        DriverId = qualifying.DriverId,
                                        TeamId = qualifying.TeamId,
                                        PointsGained = qualifying.PointsGained,
                                        Id = qualifying.Id,
                                        Position = qualifying.Position,
                                        IsReserve = qualifying.IsReserve,
                                        FastestLapInMs = qualifying.FastestLapInMs,
                                        ResultStatus = qualifying.ResultStatus,
                                        BestTimeTyre = qualifying.BestTimeTyre
                                    }).ToList(),

                                    Sprint = grandPrix.Sprints.Select(sprint => new RaceSprintDto
                                    {
                                        DriverId = sprint.DriverId,
                                        TeamId = sprint.TeamId,
                                        PointsGained = sprint.PointsGained,
                                        Id = sprint.Id,
                                        Position = sprint.Position,
                                        IsReserve = sprint.IsReserve,
                                        RaceTime = sprint.RaceTime,
                                        TimePenalty = sprint.TimePenalty,
                                        LapsCompleted = sprint.LapsCompleted,
                                        GridPosition = sprint.GridPosition,
                                        UsedTyres = sprint.UsedTyres

                                    }).ToList(),
                                    Teams = grandPrix.Races.Select(race => new TeamDto
                                    {
                                        Id = race.TeamId,
                                        Name = race.Team.Name,
                                        ImagePath = race.Team.ImagePath,
                                        ColorHex = race.Team.ColorHex
                                    }).Distinct().ToList(),

                                    Drivers = grandPrix.Races.Select(race => new DriverGrandPrixDto
                                    {
                                        Id = race.DriverId,
                                        Name = race.Driver.Name,
                                        TeamId = race.TeamId,
                                        CountryIso = race.Driver.Country.Iso
                                    }).Distinct().ToList()


                                }).FirstOrDefaultAsync();

            return await query;
        }

        public async Task<int> InsertResultsNoSprintAsync(ResultInsertDto data, int id)
        {
            var races = MyMapper<Race, RaceInsertDto>.MapList(data.Races);
            var quals = MyMapper<Qualifying, QualInsertDto>.MapList(data.Quals);

            foreach (Race race in races)
                race.GrandPrixId = id;
            foreach (Qualifying qualifying in quals)
                qualifying.GrandPrixId = id;

            try
            {
                await _context.Set<Race>().AddRangeAsync(races);
                await _context.Set<Qualifying>().AddRangeAsync(quals);
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int> InsertResultsAsync(ResultInsertDto data, int id)
        {
            var races = MyMapper<Race, RaceInsertDto>.MapList(data.Races);
            var quals = MyMapper<Qualifying, QualInsertDto>.MapList(data.Quals);
            var sprints = MyMapper<Sprint, SprintInsertDto>.MapList(data.Sprints);

            foreach (Race race in races)
                race.GrandPrixId = id;
            foreach (Qualifying qualifying in quals)
                qualifying.GrandPrixId = id;
            foreach (Sprint sprint in sprints)
                sprint.GrandPrixId = id;

            try
            {
                await _context.Set<Race>().AddRangeAsync(races);
                await _context.Set<Qualifying>().AddRangeAsync(quals);
                await _context.Set<Sprint>().AddRangeAsync(sprints);
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool HasSprint(int id)
        {
            return _context.Set<GrandPrix>().Where(d => d.Id == id).Select(p => p.HasSprint).FirstOrDefault();
        }
    }
}
