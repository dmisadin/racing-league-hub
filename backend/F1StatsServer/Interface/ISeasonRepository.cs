using F1StatsServer.Dto.SeasonDtos;

namespace F1StatsServer.Interface
{
    public interface ISeasonRepository
    {
        IQueryable<SeasonDisplayDto> GetSeasonData(int id);
    }
}
