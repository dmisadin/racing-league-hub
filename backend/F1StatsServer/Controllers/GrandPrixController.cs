using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class GrandPrixController : GenericController<GrandPrix>
    {
        public GrandPrixController(IGenericRepository<GrandPrix> genericRepository) : base(genericRepository)
        {
        }
    }
}
