using F1StatsServer.Dto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Controllers
{
    public class GameController : GenericController<Game, GameDto>
    {
      public GameController(IGenericRepository<Game> genericRepository) : base(genericRepository) { }
    }
}
