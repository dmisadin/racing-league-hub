using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class DriverController : GenericController<Driver, DriverDto>
    {
        private readonly IDriverService _driverService;

        public DriverController(IGenericRepository<Driver> genericRepository, IDriverService driverService) : base(genericRepository)
        {
            _driverService = driverService;
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> InsertData([FromBody] DriverInsertDto data)
        {
            if (data == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _driverService.InsertDataAsync(data);

            return Ok(result);
        }
    }
}
