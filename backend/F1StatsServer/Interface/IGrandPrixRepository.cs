using F1StatsServer.Dto.GrandPrixDto;

namespace F1StatsServer.Interface
{
    public interface IGrandPrixRepository
    {
        IQueryable GetData();
        List<GrandPrixPageDto> GetTrackData(int id);
    }
}
