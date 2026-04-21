using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos.GrandPrix;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers;

[Route("api/grands-prix")]
public class GrandPrixController : GenericController<GrandPrix, GrandPrixDto>
{
    public GrandPrixController(IRepository<GrandPrix> repository) : base(repository)
    {
    }

    protected override IDtoFactory<GrandPrix, GrandPrixDto> DtoFactory => new GrandPrixDtoFactory();
}
