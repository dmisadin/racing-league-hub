using F1StatsServer.Dto.GrandPrixDtos;

namespace F1StatsServer.Interface
{
    public interface IGrandPrixService
    {
        public Task<int> InsertDataAsync(List<GrandPrixInsertDto> data);
    }
}
