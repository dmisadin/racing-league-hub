using F1StatsServer.Dto.GrandPrixDtos;

namespace F1StatsServer.Interface
{
    public interface IGrandPrixService
    {
        public int InsertData(List<GrandPrixInsertDto> data);
    }
}
