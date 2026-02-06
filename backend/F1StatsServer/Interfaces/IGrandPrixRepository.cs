using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Dto.ResultsDtos;

namespace F1StatsServer.Interfaces
{
    public interface IGrandPrixRepository
    {
        Task<List<GrandPrixHomeDto>> GetDataAsync();
        Task<GrandPrixDto?> GetGrandPrixDataAsync(int id);
        bool HasSprint(int id);
        Task<List<GrandPrixHomeDto>> GetGrandPrixStartingSoon();
        Task<List<GrandPrixHomeDto>> GetGrandPrixLive();
    }
}
