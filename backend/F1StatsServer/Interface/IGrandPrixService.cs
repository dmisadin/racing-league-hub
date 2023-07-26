using F1StatsServer.Dto.GrandPrixDto;

namespace F1StatsServer.Interface
{
    public interface IGrandPrixService
    {
        public bool InsertData(List<GrandPrixInsertDto> data);
    }
}
