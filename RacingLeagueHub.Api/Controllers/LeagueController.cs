using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Api.Controllers;

[Route("api/leagues")]
public class LeagueController : GenericController<League, LeagueDto>
{
    private readonly ILeagueRepository leagueRepository;

    public LeagueController(ILeagueRepository repository) : base(repository)
    {
        this.leagueRepository = repository;
    }

    protected override IDtoFactory<League, LeagueDto> DtoFactory => new LeagueDtoFactory();

    [HttpGet("get-by-slug/{slug}")]
    public async Task<ActionResult<LeagueDto>> GetBySlug(string slug)
    {
        if (string.IsNullOrEmpty(slug))
            return BadRequest();

        var league = await this.leagueRepository.GetBySlugAsync<LeagueDto?>(slug, DtoFactory.ToDtoExpression());

        if (league == null)
            return NotFound(slug);

        return Ok(league);
    }
}
