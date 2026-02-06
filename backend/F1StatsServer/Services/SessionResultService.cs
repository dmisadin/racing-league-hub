using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Interfaces;
using F1StatsServer.Repositories;

namespace F1StatsServer.Services
{
    public class SessionResultService : ISessionResultService
    {
        private readonly SessionResultRepository _sessionResultRepository;
        public SessionResultService(SessionResultRepository sessionResultRepository)
        {
            _sessionResultRepository = sessionResultRepository;
        }

        public async Task<bool> InsertResultsAsync(List<SessionResultDto> data)
        {
            return await _sessionResultRepository.InsertResultsAsync(data);
        }
    }
}
