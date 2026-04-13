using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers;

[Route("api/seasons")]
public class SeasonController : GenericController<Season, SeasonDto>
{
    private readonly ISeasonRepository seasonRepository;

    public SeasonController(IRepository<Season> repository, 
                            ISeasonRepository seasonRepository) 
        : base(repository)
    {
        this.seasonRepository = seasonRepository;
    }

    protected override IDtoFactory<Season, SeasonDto> DtoFactory => new SeasonDtoFactory();
}
