using F1StatsServer.Dto.SeasonDtos;

namespace F1StatsServer.Interface
{
    public interface ISeasonService
    {
        bool InsertSeason(SeasonInsertDto data);
    }
}
