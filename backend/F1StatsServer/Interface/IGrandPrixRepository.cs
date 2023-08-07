using F1StatsServer.Dto.GrandPrixDtos;

namespace F1StatsServer.Interface
{
    public interface IGrandPrixRepository
    {
        IQueryable GetData();
        GrandPrixDisplayDto GetTrackData(int id);
    }
}
