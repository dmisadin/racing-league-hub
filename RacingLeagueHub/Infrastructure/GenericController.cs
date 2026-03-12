using RacingLeagueHub.BLL.Mapping.DtoFactories;
using RacingLeagueHub.BLL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Models.Dtos;

namespace RacingLeagueHub.Infrastructure;

[Route("api/[controller]")]
[ApiController]
public abstract class GenericController<TEntity, TDto> : Controller
    where TEntity : IEntity
    where TDto : BaseDto
{
    protected readonly IRepository<TEntity> repository;

    public GenericController(IRepository<TEntity> genericRepository)
    {
        this.repository = genericRepository;
    }

    protected abstract IDtoFactory<TEntity, TDto> DtoFactory { get; }

    [HttpGet("get-by-id/{id}")]
    public virtual async Task<ActionResult<TDto>> GetOne([FromRoute] long id)
    {
        var entity = await repository.FindAsync(id);

        if (entity == null)
            return NotFound();

        var dto = DtoFactory.ToDto(entity);

        return Ok(dto);
    }

    [HttpPost("add")]
    public virtual async Task<ActionResult<long>> Add(TDto dto)
    {
        var entity = repository.Create();
        DtoFactory.FromDto(entity, dto);

        await this.repository.InsertAsync(entity);
        await this.repository.CommitAsync();

        return Ok(entity.Id);
    }

    [HttpPost("update")]
    public virtual async Task<ActionResult<long>> Update([FromBody] TDto dto, [FromQuery] long id)
    {
        var entity = this.repository.FindAsync(id).Result;

        if (entity == null)
            return NotFound();

        DtoFactory.FromDto(entity, dto);

        return Ok(entity.Id);
    }

    [HttpDelete("delete")]
    public virtual async Task<IActionResult> Delete(long id)
    {
        var rows = await repository.Query()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return rows == 0 ? NotFound() : NoContent();
    }
}
