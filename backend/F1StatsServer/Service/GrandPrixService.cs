using AutoMapper;
using F1StatsServer.Data;
using F1StatsServer.Dto.GrandPrixDto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;

namespace F1StatsServer.Service
{
    public class GrandPrixService : IGrandPrixService
    {
        private readonly IGenericRepository<GrandPrix> _genericRepository;
        public GrandPrixService(IGenericRepository<GrandPrix> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool InsertData(List<GrandPrixInsertDto> data)
        {
            var item = MyMapper<GrandPrix, GrandPrixInsertDto>.MapList(data);

            return _genericRepository.CreateItemList(item);
        }
    }
}
