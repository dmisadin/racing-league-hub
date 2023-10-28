using F1StatsServer.Dto.SeasonDtos;

namespace F1StatsServer.Interface
{
    public interface ISeasonRepository
    {
        Task<SeasonDisplayDto> GetSeasonData(int id);
        Task<SeasonSessionPointsDto> GetSeasonSessionPoints(int id);
    }
}
