using F1StatsServer.Dto.SeasonDtos;

namespace F1StatsServer.Interface
{
    public interface ISeasonService
    {
        int InsertSeason(SeasonInsertDto data);
        Task<SeasonDisplayDto> GetSeasonData(int id);
    }
}
