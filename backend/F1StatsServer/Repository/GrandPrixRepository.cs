using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.GrandPrixDto;
using F1StatsServer.Dto.ResultsDtos;
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

        public List<GrandPrixPageDto> GetTrackData(int id)
        {
            var query = _context.Set<GrandPrix>()
                                .Where(c => c.Id == id)
                                .Select(p => new GrandPrixPageDto
                                {
                                    GrandPrixName = p.Name,
                                    GrandPrixDate = p.StartTime,
                                    YoutubeUrl = p.YouTubeUrl,
                                    Track = new TrackDto
                                    {
                                        TrackName = p.Track.Name,
                                        Turns = p.Track.CornersTotal,
                                        LeftTurns = p.Track.CornersLeft,
                                        Elevation = p.Track.Elevation,
                                        Length = p.Track.Length
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
                                                                   TeamName = p.Team.Name,
                                                                   DriverName = p.Driver.Name,
                                                                   DriverCountry = p.Driver.Country.Iso,
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
                                                                               TeamName = p.Team.Name,
                                                                               DriverName = p.Driver.Name,
                                                                               DriverCountry = p.Driver.Country.Iso,
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
                                                                       TeamName = p.Team.Name,
                                                                       DriverName = p.Driver.Name,
                                                                       DriverCountry = p.Driver.Country.Iso,
                                                                       RaceTime = p.RaceTime,
                                                                       TimePenalty = p.TimePenalty,
                                                                       LapsCompleted = p.LapsCompleted,
                                                                       GridPosition = p.GridPosition,
                                                                       UsedTyres = p.UsedTyres

                                                                   }).ToList()

                                }).ToList();

            return query;
        }
    }
}
