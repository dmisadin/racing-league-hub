using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Infrastructure;

namespace RacingLeagueHub.API.Controllers;

public class DriverController : GenericController<Driver, DriverDto>
{
    public DriverController(IRepository<Driver> driverRepository) : base(driverRepository)
    {
    }

    protected override IDtoFactory<Driver, DriverDto> DtoFactory => new DriverDtoFactory();
}
