using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Entities;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Utility;

namespace F1StatsServer.Services
{
    public class GrandPrixService : IGrandPrixService
    {
        private readonly IGenericRepository<GrandPrix> _genericRepository;
        public GrandPrixService(IGenericRepository<GrandPrix> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> InsertDataAsync(List<GrandPrixInsertDto> data)
        {
            var item = MyMapper<GrandPrix, GrandPrixInsertDto>.MapList(data);

            return await _genericRepository.CreateItemListAsync(item);
        }
    }
}
