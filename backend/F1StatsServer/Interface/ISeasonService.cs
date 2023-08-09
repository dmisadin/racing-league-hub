using F1StatsServer.Dto.SeasonDtos;

namespace F1StatsServer.Interface
{
    public interface ISeasonService
    {
        Task<int> InsertSeasonAsync(SeasonInsertDto data);
        Task<SeasonDisplayDto> GetSeasonDataAsync(int id);
    }
}
