using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;

namespace F1StatsServer.Services
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
