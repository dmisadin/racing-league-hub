using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class RaceController : GenericController<Race, DriverDto>
    {
        public RaceController(IGenericRepository<Race> genericRepository) : base(genericRepository)
        {
        }
    }
}
