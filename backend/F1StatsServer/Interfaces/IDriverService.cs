using F1StatsServer.Dto.DriverDtos;

namespace F1StatsServer.Interfaces
{
    public interface IDriverService
    {
        Task<int> InsertDataAsync(DriverInsertDto data);
    }
}