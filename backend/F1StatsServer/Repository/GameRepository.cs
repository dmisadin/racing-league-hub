using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class GameRepository : GenericRepository<Game>, IGenericRepository<Game>
    {
        public GameRepository(AdventureContext context) : base(context) { }
    }
}
