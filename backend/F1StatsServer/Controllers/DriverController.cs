using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class DriverController : GenericController<Driver>
    {
        public DriverController(IGenericRepository<Driver> genericRepository) : base(genericRepository)
        {

        }
    }
}
