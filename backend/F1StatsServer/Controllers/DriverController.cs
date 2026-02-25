using F1StatsServer.BLL.Mapping.DtoFactories;
using F1StatsServer.BLL.Models.Dtos;
using F1StatsServer.Entities;
using F1StatsServer.Infrastructure;

namespace F1StatsServer.Controllers;

public class DriverController : GenericController<Driver, DriverDto>
{
    public DriverController(IRepository<Driver> driverRepository) : base(driverRepository)
    {
    }

    protected override IDtoFactory<Driver, DriverDto> DtoFactory => new DriverDtoFactory();
}
