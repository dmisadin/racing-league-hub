using F1StatsServer.Dto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Service
{
    public class LeagueService : ILeagueService
    {
        private readonly IGenericRepository<League> _genericRepository;

        public LeagueService(IGenericRepository<League> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool InsertLeague(LeagueInsertDto data)
        {
            var item = new League
            {
                Name = data.Name,
                Description = data.Description,
                ImagePath = data.ImagePath,
                RegionId = data.RegionId,
                ColorHex = data.ColorHex,
                SocialMedia = new SocialMedium
                {
                    Website = data.Website,
                    Discord = data.Discord,
                    YouTube = data.YouTube,
                    Twitch = data.Twitch,
                    Facebook = data.Facebook,
                    Instagram = data.Instagram,
                }
            };
            if (item == null)
                return false;

            return _genericRepository.CreateItem(item);
        }
    }
}
