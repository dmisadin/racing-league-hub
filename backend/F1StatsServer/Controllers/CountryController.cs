using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class CountryController : GenericController<Country>
    {
        public CountryController(IGenericRepository<Country> genericRepository) : base(genericRepository)
        {
        }

    }
}
