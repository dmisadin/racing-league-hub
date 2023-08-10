using F1StatsServer.Dto.DriverDtos;

namespace F1StatsServer.Interface
{
    public interface IDriverService
    {
        Task<int> InsertDataAsync(DriverInsertDto data);
    }
}