using F1StatsServer.Dto.LeagueDto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Service
{
    public class LeagueService : ILeagueService
    {
        private readonly IGenericRepository<League> _genericRepository;
        private readonly ILeagueRepository _leagueRepository;

        public LeagueService(IGenericRepository<League> genericRepository, ILeagueRepository leagueRepository)
        {
            _genericRepository = genericRepository;
            _leagueRepository = leagueRepository;
        }

        //TODO: Swap SocialMedium initializer to use MyMapper<TDto,T>.MapList(data)
        public int InsertLeague(LeagueInsertDto data)
        {
            var item = new League
            {
                Name = data.Name,
                Description = data.Description,
                ImagePath = data.ImagePath,
                RegionId = data.RegionId,
                ColorHex = data.ColorHex,
                SocialMedia = new SocialMedia
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
                return -1;

            return _genericRepository.CreateItem(item);
        }

        public LeagueDisplayDto GetLeagueData(int id)
        {
            if (!_genericRepository.Has(id))
                return null;

            var item = _leagueRepository.GetLeagueData(id);
            var itemQueried = item.FirstOrDefault();

            if (itemQueried == null)
                return null;

            return itemQueried;
        }

    }
}
