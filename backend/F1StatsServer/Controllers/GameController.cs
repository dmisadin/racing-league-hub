using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class GameController : GenericController<Game, GameDto>
    {
      public GameController(IGenericRepository<Game> genericRepository) : base(genericRepository) { }
    }
}
