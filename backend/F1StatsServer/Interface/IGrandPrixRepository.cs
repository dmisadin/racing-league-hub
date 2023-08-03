using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Dto.ResultsDtos;

namespace F1StatsServer.Interface
{
    public interface IGrandPrixRepository
    {
        IQueryable GetData();
        List<GrandPrixDisplayDto> GetTrackData(int id);
        bool HasSprint(int id);
        int InsertResults(ResultInsertDto data, int id);
        int InsertResultsNoSprint(ResultInsertDto data, int id);
    }
}
