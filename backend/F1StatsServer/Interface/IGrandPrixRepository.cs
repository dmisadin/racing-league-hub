namespace F1StatsServer.Interface
{
    public interface IGrandPrixRepository
    {
        IQueryable GetData();
        IQueryable GetTrackData(int id);
    }
}
