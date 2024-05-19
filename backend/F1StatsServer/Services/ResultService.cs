using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;
using F1StatsServer.Utility;

namespace F1StatsServer.Services
{
    public class ResultService : IResultService
    {
        private readonly IGrandPrixRepository _grandPrixRepository;
        public ResultService(IGrandPrixRepository grandPrixRepository)
        {
            _grandPrixRepository = grandPrixRepository;
        }

        public async Task<int> InsertResultsAsync(ResultInsertDto data, int grandPrixId)
        {
            int result;

            if (_grandPrixRepository.HasSprint(grandPrixId) && (data.Sprints != null || data.Sprints.Count > 0 ))
                result = await _grandPrixRepository.InsertResultsAsync(data, grandPrixId);
            else
                result = await _grandPrixRepository.InsertResultsNoSprintAsync(data, grandPrixId);

            return result;
        }
    }
}
