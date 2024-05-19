using F1StatsServer.Dto.ResultsDtos;

namespace F1StatsServer.Interfaces
{
    public interface IResultService
    {
        Task<int> InsertResultsAsync(ResultInsertDto data, int grandPrixId);
    }
}
