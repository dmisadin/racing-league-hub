using F1StatsServer.Data;
using F1StatsServer.Dto;
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
            var query = (from GrandPrix in _context.Set<GrandPrix>()
                         join Season in _context.Set<Season>() on GrandPrix.SeasonId equals Season.Id
                         join League in _context.Set<League>() on Season.LeagueId equals League.Id
                         select new GrandPrixHomeDto { Id = GrandPrix.Id, GrandPrixName = GrandPrix.Name, SeasonName = Season.Name, LeagueName = League.Name });
                        
            return query;            
        }

        public IQueryable GetTrackData(int id)
        {
            var query = from GrandPrix in _context.Set<GrandPrix>()
                        join Track in _context.Set<Track>() on GrandPrix.TrackId equals Track.Id
                        where GrandPrix.Id == id
                        select new GrandPrixPageDto
                        {
                            GrandPrixName = GrandPrix.Name,
                            GrandPrixDate = GrandPrix.StartTime,
                            YoutubeUrl = GrandPrix.YouTubeUrl,
                            Track = new TrackDto
                            {
                                TrackName = Track.Name,
                                Turns = Track.CornersTotal,
                                LeftTurns = Track.CornersLeft,
                                Elevation = Track.Elevation,
                                Length = Track.Length
                            },
                            Race = (from Race in _context.Set<Race>()
                                    join Team in _context.Set<Team>() on Race.TeamId equals Team.Id
                                    join Driver in _context.Set<Driver>() on Race.DriverId equals Driver.Id
                                    where Race.GrandPrixId == id
                                    select new RaceDto
                                    {
                                        Position = Race.Position,
                                        TeamName = Team.Name,
                                        DriverName = Driver.Name,
                                        RaceTime = Race.RaceTime,
                                        Penalty = Race.TimePenalty,
                                        Points = Race.PointsGained,
                                        Tyres = Race.UsedTyres
                                    }).ToList()

                        };
            //var query = _context.Set<GrandPrix>()
            //                    .AsSplitQuery()
            //                    .Where(c => c.Id == id)
            //                    .Select (p => new GrandPrixPageDto
            //                    {
            //                        GrandPrixName = p.Name,
            //                        GrandPrixDate = p.StartTime,
            //                        YoutubeUrl = p.YouTubeUrl,
            //                        Track = new TrackDto
            //                        {
            //                            TrackName = p.Track.Name,
            //                            Turns = p.Track.CornersTotal,
            //                            LeftTurns = p.Track.CornersLeft,
            //                            Elevation = p.Track.Elevation,
            //                            Length = p.Track.Length
            //                        },
            //                        Race = _context.Set<Race>().Where(c => c.GrandPrixId == id)
            //                                                   .Select(p => new RaceDto
            //                                                   {
            //                                                       Position = p.Position,
            //                                                       TeamName = p.Team.Name,
            //                                                       DriverName = p.Driver.Name,
            //                                                       RaceTime = p.RaceTime,
            //                                                       Penalty = p.TimePenalty,
            //                                                       Points = p.PointsGained,
            //                                                       Tyres = p.UsedTyres
            //                                                   }).ToList()

            //                    });

            return query;
        }
    }
}
