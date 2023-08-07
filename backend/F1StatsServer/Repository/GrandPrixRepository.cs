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

        public IQueryable GetData()
        {
            var query = from GrandPrix in _context.Set<GrandPrix>()
                        join Season in _context.Set<Season>() on GrandPrix.SeasonId equals Season.Id
                        join League in _context.Set<League>() on Season.LeagueId equals League.Id
                        select new GrandPrixHomeDto { 
                            Id = GrandPrix.Id,
                            GrandPrixName = GrandPrix.Name,
                            SeasonName = Season.Name,
                            LeagueName = League.Name
                        };
                        
            return query;            
        }

        public GrandPrixDisplayDto? GetGrandPrixData(int id)
        {
            var query = _context.Set<GrandPrix>()
                                .AsSplitQuery()
                                .Where(c => c.Id == id)
                                .Select(p => new GrandPrixDisplayDto
                                {
                                    GrandPrixName = p.Name,
                                    GrandPrixDate = p.StartTime,
                                    YoutubeUrl = p.YoutubeUrl,
                                    FastestDriverId = p.Races.Where(g => g.FastestLapInMs != null)
                                                                                       .OrderBy(e => e.FastestLapInMs).Select(f => f.DriverId).FirstOrDefault(),
                                    Track = new TrackDto
                                    {
                                        Id = p.Track.Id,
                                        Name = p.Track.Name,
                                        Location = p.Track.Location,
                                        CornersTotal = p.Track.CornersTotal,
                                        CornersLeft = p.Track.CornersLeft,
                                        Elevation = p.Track.Elevation,
                                        Length = p.Track.Length,
                                        PitStop = p.Track.PitStop,
                                        ImagePath = p.Track.ImagePath,
                                        Laps = p.Track.Laps,
                                        Country = new CountryDto
                                        {
                                            Id = p.Track.Country.Id,
                                            NameCroatian = p.Track.Country.NameCroatian,
                                            NameEnglish = p.Track.Country.NameEnglish,
                                            Iso = p.Track.Country.Iso,
                                            Iso3 = p.Track.Country.Iso3,
                                            ImagePath = p.Track.Country.ImagePath
                                        }

                                    },
                                    Race = _context.Set<Race>().Where(c => c.GrandPrixId == id)
                                                               .Select(p => new RaceDto
                                                               {
                                                                   DriverId = p.DriverId,
                                                                   TeamId = p.TeamId,
                                                                   PointsGained = p.PointsGained,
                                                                   Id = p.Id,
                                                                   Position = p.Position,
                                                                   IsReserve = p.IsReserve,
                                                                   RaceTime = p.RaceTime,
                                                                   TimePenalty = p.TimePenalty,
                                                                   LapsCompleted = p.LapsCompleted,
                                                                   GridPosition = p.GridPosition,
                                                                   UsedTyres = p.UsedTyres,
                                                                   FastestLapInMs = p.FastestLapInMs,
                                                                   ResultStatus = p.ResultStatus,
                                                                   PostRaceTimePenalty = p.PostRaceTimePenalty

                                                               }).ToList(),
                                    Qualifying = _context.Set<Qualifying>().Where(c => c.GrandPrixId == id)
                                                                           .Select(p => new QualDto
                                                                           {
                                                                               DriverId = p.DriverId,
                                                                               TeamId = p.TeamId,
                                                                               PointsGained = p.PointsGained,
                                                                               Id = p.Id,
                                                                               Position = p.Position,
                                                                               IsReserve = p.IsReserve,
                                                                               FastestLapInMs = p.FastestLapInMs,
                                                                               ResultStatus = p.ResultStatus,
                                                                               BestTimeTyre = p.BestTimeTyre

                                                                           }).ToList(),
                                    Sprint = _context.Set<Sprint>().Where(c => c.GrandPrixId == id)
                                                                   .Select(p => new RaceSprintDto
                                                                   {
                                                                       DriverId = p.DriverId,
                                                                       TeamId = p.TeamId,
                                                                       PointsGained = p.PointsGained,
                                                                       Id = p.Id,
                                                                       Position = p.Position,
                                                                       IsReserve = p.IsReserve,
                                                                       RaceTime = p.RaceTime,
                                                                       TimePenalty = p.TimePenalty,
                                                                       LapsCompleted = p.LapsCompleted,
                                                                       GridPosition = p.GridPosition,
                                                                       UsedTyres = p.UsedTyres

                                                                   }).ToList(),
                                    Teams = _context.Set<Race>().Where(c => c.GrandPrixId == id)
                                                                .Select(p => new TeamDto
                                                                {
                                                                    Id = p.TeamId,
                                                                    Name = p.Team.Name,
                                                                    ImagePath = p.Team.ImagePath,
                                                                    ColorHex = p.Team.ColorHex
                                                                }).Distinct().ToList(),

                                    Drivers = _context.Set<Race>().Where(c => c.GrandPrixId == id)
                                                                .Select(p => new DriverGrandPrixDto
                                                                {
                                                                   Id = p.DriverId,
                                                                   Name = p.Driver.Name,
                                                                   TeamId = p.TeamId,
                                                                   CountryIso = p.Driver.Country.Iso
                                                                }).Distinct().ToList()


                                }).FirstOrDefault();

            return query;
        }

        public int InsertResultsNoSprint(ResultInsertDto data, int id)
        {
            var races = MyMapper<Race, RaceInsertDto>.MapList(data.Races);
            var quals = MyMapper<Qualifying, QualInsertDto>.MapList(data.Quals);

            foreach (Race race in races)
                race.GrandPrixId = id;
            foreach (Qualifying qualifying in quals)
                qualifying.GrandPrixId = id;

            try
            {
                _context.Set<Race>().AddRange(races);
                _context.Set<Qualifying>().AddRange(quals);
                var result = _context.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int InsertResults(ResultInsertDto data, int id)
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
                _context.Set<Race>().AddRange(races);
                _context.Set<Qualifying>().AddRange(quals);
                _context.Set<Sprint>().AddRange(sprints);
                var result = _context.SaveChanges();
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
