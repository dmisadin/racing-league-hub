using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.LeagueDtos;
using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Repository
{
    public class LeagueRepository : GenericRepository<League>, IGenericRepository<League>, ILeagueRepository
    {
        private readonly AdventureContext _context;
        public LeagueRepository(AdventureContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LeaguesDisplayDto>> GetLeaguesAsync()
        {
            var query = _context.Set<League>()
                                .AsNoTracking()
                                .Select(league => new LeaguesDisplayDto
                                {
                                    Id = league.Id,
                                    Name = league.Name,
                                    ColorHex = league.ColorHex,
                                    ImagePath = league.ImagePath,
                                    Platform = league.Seasons.OrderByDescending(season => season.GrandPrixes.OrderByDescending(grandPrix => grandPrix.StartTime)
                                                                                                            .Take(1).Select(grandPrix => grandPrix.StartTime)
                                                                                                            .FirstOrDefault())
                                                                                                            .Take(1)
                                                                                                            .Select(season => season.Platform).FirstOrDefault(),
                                    Game = league.Seasons.OrderByDescending(season => season.GrandPrixes.OrderByDescending(grandPrix => grandPrix.StartTime)
                                                                                                        .Take(1).Select(grandPrix => grandPrix.StartTime)
                                                                                                        .FirstOrDefault())
                                                                                                        .Take(1)
                                                                                                        .Select(season => season.Game).FirstOrDefault(),
                                }).ToListAsync();

            return await query;
        }

        public async Task<LeagueDisplayDto> GetLeagueDataAsync(int id)
        {
            var query = _context.Set<League>()
                                .AsNoTracking()
                                .Where(league => league.Id == id)
                                .Select(league => new LeagueDisplayDto
                                {
                                    Name = league.Name,
                                    Description = league.Description,
                                    ColorHex = league.ColorHex,
                                    ImagePath = league.ImagePath,
                                    SocialMedia = new SocialMediaDto
                                    {
                                        Website = league.SocialMedia.Website,
                                        Discord = league.SocialMedia.Discord,
                                        Youtube = league.SocialMedia.Youtube,
                                        Twitch = league.SocialMedia.Twitch,
                                        Twitter = league.SocialMedia.Twitter,
                                        Instagram = league.SocialMedia.Instagram,
                                        Facebook = league.SocialMedia.Facebook
                                    },
                                    Region = league.Region,
                                    SeasonsInLeague = league.Seasons
                                                      .Select(season => new SeasonsInLeagueDto
                                                      {
                                                          Id = season.Id,
                                                          Name = season.Name,
                                                          ImagePath = season.ImagePath,
                                                          Game = season.Game,
                                                          Platform = season.Platform,
                                                          StartTime = season.GrandPrixes.OrderBy(grandPrix => grandPrix.StartTime)
                                                                                   .Select(grandPrix => grandPrix.StartTime).FirstOrDefault(),
                                                          EndTime = season.GrandPrixes.OrderByDescending(grandPrix => grandPrix.StartTime)
                                                                                   .Select(grandPrix => grandPrix.StartTime).FirstOrDefault()
                                                      }).ToList(),

                                }).FirstOrDefaultAsync();
            return await query;
        }

    }
}
