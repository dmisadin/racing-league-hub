using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Service
{
    public class DriverService : IDriverService
    {
        private readonly IGenericRepository<Driver> _genericRepository;

        public DriverService(IGenericRepository<Driver> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> InsertDataAsync(DriverInsertDto data)
        {
            Driver item = new Driver
            {
                Name = data.Name,
                CountryId = data.CountryId,
                PlatformId = data.PlatformId
            };

            return await _genericRepository.CreateItemAsync(item);

        }
    }
}
