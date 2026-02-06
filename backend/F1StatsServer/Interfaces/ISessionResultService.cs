using F1StatsServer.Dto.ResultsDtos;

namespace F1StatsServer.Interfaces
{
    public interface ISessionResultService
    {
        Task<bool> InsertResultsAsync(List<SessionResultDto> data);
    }
}
