using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.GrandPrixDto;
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
                                                                   Position = p.Position,
                                                                   TeamName = p.Team.Name,
                                                                   DriverName = p.Driver.Name,
                                                                   RaceTime = p.RaceTime,
                                                                   Penalty = p.TimePenalty,
                                                                   Points = p.PointsGained,
                                                                   Tyres = p.UsedTyres
                                                               }).ToList()

                                }).ToList();

            return query;
        }
    }
}
