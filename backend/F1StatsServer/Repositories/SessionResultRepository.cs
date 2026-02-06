using F1StatsServer.Data;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Entities;
using F1StatsServer.Infrastructure;
using F1StatsServer.Utility;

namespace F1StatsServer.Repositories
{
    public class SessionResultRepository : GenericRepository<SessionResult>, IGenericRepository<SessionResult>
    {
        public SessionResultRepository(AdventureContext context) : base(context) { }


        public async Task<bool> InsertResultsAsync(List<SessionResultDto> data)
        {
            var results = MyMapper<SessionResult, SessionResultDto>.MapList(data);
            try
            {
                Remove(results.Where(r => r.SelectedForDeletion).ToArray());
                Update(results.Where(r => r.Id != 0 && !r.SelectedForDeletion).ToArray());
                await Insert(results.Where(r => r.Id == 0 && !r.SelectedForDeletion).ToArray());
                return await Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
