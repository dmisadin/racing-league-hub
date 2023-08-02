using F1StatsServer.Dto.GrandPrixDtos;

namespace F1StatsServer.Interface
{
    public interface IGrandPrixRepository
    {
        IQueryable GetData();
        List<GrandPrixDisplayDto> GetTrackData(int id);
    }
}
