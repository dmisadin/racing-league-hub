using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.API.DtoFactories;
using RacingLeagueHub.API.DtoFactories.Admin;
using RacingLeagueHub.API.Dtos.Track;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Models.Constants;
using RacingLeagueHub.Infrastructure;

namespace RacingLeagueHub.API.Controllers.Admin;

[Route("api/track")]
[ApiController]
public class TrackController : GenericController<Track, TrackDto>
{
    public TrackController(IRepository<Track> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoFactory<Track, TrackDto> DtoFactory => new TrackDtoFactory();

    [HttpGet("get-all")]
    public virtual async Task<ActionResult<List<TrackDto>>> GetAll()
    {
        var dtos = await repository.Query()
            .Select(DtoFactory.ToDtoExpression())
            .ToListAsync();

        if (dtos == null)
            return NotFound();

        foreach (var dto in dtos)
        {
            if (Countries.ByAlpha2.TryGetValue(dto.CountryAlpha2, out var country))
                dto.Country = country;
        }

        return Ok(dtos);
    }
}
