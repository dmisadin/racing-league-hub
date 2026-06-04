using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers;

public class DriverController : GenericController<Driver, DriverDto>
{
    public DriverController(IRepository<Driver> repository,
        IDtoMapper<Driver, DriverDto> dtoMapper) : base(repository, dtoMapper)
    {
    }
}
