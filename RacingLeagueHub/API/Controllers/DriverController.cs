using RacingLeagueHub.BLL.Mapping.DtoFactories;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.Infrastructure;
using RacingLeagueHub.API.Dtos;

namespace RacingLeagueHub.API.Controllers;

public class DriverController : GenericController<Driver, DriverDto>
{
    public DriverController(IRepository<Driver> driverRepository) : base(driverRepository)
    {
    }

    protected override IDtoFactory<Driver, DriverDto> DtoFactory => new DriverDtoFactory();
}
