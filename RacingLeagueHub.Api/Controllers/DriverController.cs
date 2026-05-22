using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers;

public class DriverController : GenericController<Driver, DriverDto>
{
    public DriverController(IRepository<Driver> driverRepository) : base(driverRepository)
    {
    }

    protected override IDtoMapper<Driver, DriverDto> DtoFactory => new DriverDtoFactory();
}
