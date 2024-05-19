using F1StatsServer.Dto.GrandPrixDtos;

namespace F1StatsServer.Interfaces
{
    public interface IGrandPrixService
    {
        public Task<int> InsertDataAsync(List<GrandPrixInsertDto> data);
    }
}
