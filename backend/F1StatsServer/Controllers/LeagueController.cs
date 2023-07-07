using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class LeagueController : GenericController<League, LeagueDto>
    {
    public LeagueController(IGenericRepository<League> genericRepository) : base(genericRepository)
    {
    }
}
}
