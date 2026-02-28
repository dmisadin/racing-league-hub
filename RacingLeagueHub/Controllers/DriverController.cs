using RacingLeagueHub.BLL.Mapping.DtoFactories;
using RacingLeagueHub.BLL.Models.Dtos;
using RacingLeagueHub.Entities;
using RacingLeagueHub.Infrastructure;

namespace RacingLeagueHub.Controllers;

public class DriverController : GenericController<Driver, DriverDto>
{
    public DriverController(IRepository<Driver> driverRepository) : base(driverRepository)
    {
    }

    protected override IDtoFactory<Driver, DriverDto> DtoFactory => new DriverDtoFactory();
}
