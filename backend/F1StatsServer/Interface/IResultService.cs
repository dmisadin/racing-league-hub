using F1StatsServer.Dto.ResultsDtos;

namespace F1StatsServer.Interface
{
    public interface IResultService
    {
        int InsertResults(ResultInsertDto data, int grandPrixId);
    }
}
