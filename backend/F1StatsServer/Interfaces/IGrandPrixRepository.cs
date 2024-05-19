using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Dto.ResultsDtos;

namespace F1StatsServer.Interfaces
{
    public interface IGrandPrixRepository
    {
        Task<List<GrandPrixHomeDto>> GetDataAsync();
        Task<GrandPrixDisplayDto?> GetGrandPrixDataAsync(int id);
        bool HasSprint(int id);
        Task<int> InsertResultsAsync(ResultInsertDto data, int id);
        Task<int> InsertResultsNoSprintAsync(ResultInsertDto data, int id);
        Task<List<GrandPrixHomeDto>> GetGrandPrixStartingSoon();
        Task<List<GrandPrixHomeDto>> GetGrandPrixLive();
    }
}
