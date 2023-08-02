using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.LeagueDtos;
using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;

namespace F1StatsServer.Repository
{
    public class LeagueRepository : GenericRepository<League>, IGenericRepository<League>, ILeagueRepository
    {
        private readonly AdventureContext _context;
        public LeagueRepository(AdventureContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<LeagueDisplayDto> GetLeagueData(int id)
        {
            var query = _context.Set<League>()
                                .Where(c => c.Id == id)
                                .Select(p => new LeagueDisplayDto
                                {
                                    Name = p.Name,
                                    Description = p.Description,
                                    ColorHex = p.ColorHex,
                                    ImagePath = p.ImagePath,
                                    SocialMedia = _context.Set<SocialMedia>()
                                                          .Where(c => c.Id == p.SocialMediaId)
                                                          .Select(d => new SocialMediaDto
                                                          {
                                                              Website = d.Website,
                                                              Discord = d.Discord,
                                                              Youtube = d.Youtube,
                                                              Twitch = d.Twitch,
                                                              Twitter = d.Twitter,
                                                              Instagram = d.Instagram,
                                                              Facebook = d.Facebook,
                                                          }).FirstOrDefault(),
                                    Region = _context.Set<Region>()
                                                     .Where(c => c.Id == p.RegionId)
                                                     .Select(d => new RegionDto
                                                     {
                                                         Id = d.Id,
                                                         Name = d.Name
                                                     }).FirstOrDefault(),
                                    SeasonsInLeague = p.Seasons
                                                      .Where(c => c.LeagueId == id)
                                                      .Select(d => new SeasonsInLeagueDto
                                                      {
                                                            Id = d.Id,
                                                            Name = d.Name,
                                                            ImagePath = d.ImagePath,
                                                            Game = _context.Set<Game>().Where(e => e.Id == d.GameId)
                                                                                       .Select(f => new GameDto
                                                                                       {
                                                                                           Id = f.Id,
                                                                                           Name = f.Name
                                                                                       }).FirstOrDefault(),
                                                            Platform = _context.Set<Platform>().Where(e => e.Id == d.PlatformId)
                                                                                       .Select(f => new PlatformDto
                                                                                       {
                                                                                           Id = f.Id,
                                                                                           Name = f.Name
                                                                                       }).FirstOrDefault(),
                                                          StartTime = _context.Set<GrandPrix>().Where(e => e.SeasonId == d.Id)
                                                                                                .OrderBy(f => f.StartTime)
                                                                                                .Select(g => g.StartTime).FirstOrDefault(),
                                                            EndTime = _context.Set<GrandPrix>().Where(e => e.SeasonId == d.Id)
                                                                                                .OrderByDescending(f => f.StartTime)
                                                                                                .Select(g => g.StartTime).FirstOrDefault(),

                                                      }).ToList(),

                                });
            return query;
        }

    }
}
