using F1StatsServer.Dto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class CountryController : GenericController<Country, CountryDto>
    {
        public CountryController(IGenericRepository<Country> genericRepository) : base(genericRepository)
        {
        }

    }
}
