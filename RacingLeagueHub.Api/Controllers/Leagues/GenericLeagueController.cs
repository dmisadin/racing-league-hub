using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers.Leagues;

public abstract class GenericLeagueController<TEntity, TDto> : GenericController<TEntity, TDto>
    where TEntity : IEntity
    where TDto : BaseDto
{
    protected GenericLeagueController(IRepository<TEntity> repository) : base(repository)
    {
    }

    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public override Task<ActionResult<EncryptedId>> Add([FromBody] TDto dto)
    {
        return base.Add(dto);
    }

    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public override Task<ActionResult<EncryptedId>> Update([FromBody] TDto dto)
    {
        return base.Update(dto);
    }

    [Authorize(Policy = LeaguePolicies.LeagueOwner)]
    public override Task<IActionResult> Delete([FromRoute] EncryptedId id)
    {
        return base.Delete(id);
    }
}
