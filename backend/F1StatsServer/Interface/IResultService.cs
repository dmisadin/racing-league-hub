using F1StatsServer.Dto.ResultsDtos;

namespace F1StatsServer.Interface
{
    public interface IResultService
    {
        Task<int> InsertResultsAsync(ResultInsertDto data, int grandPrixId);
    }
}
