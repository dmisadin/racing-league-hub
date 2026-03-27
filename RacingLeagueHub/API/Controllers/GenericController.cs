using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.API.DtoFactories;
using RacingLeagueHub.API.Dtos;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Infrastructure;

namespace RacingLeagueHub.API.Controllers;

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
        var dto = await repository.Query()
                                .Where(e => e.Id == id)
                                .Select(DtoFactory.ToDtoExpression())
                                .FirstOrDefaultAsync();

        if (dto == null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost("add")]
    public virtual async Task<ActionResult<long>> Add([FromBody] TDto dto)
    {
        var entity = repository.Create();
        DtoFactory.FromDto(entity, dto);

        await this.repository.InsertAsync(entity);
        await this.repository.CommitAsync();

        return Ok(entity.Id);
    }

    [HttpPost("update")]
    public virtual async Task<ActionResult<long>> Update([FromBody] TDto dto)
    {
        var id = dto.Id;
        if (id == null || id == 0)
            return BadRequest("Invalid ID.");

        var entity = await this.repository.FindAsync(id);

        if (entity == null)
            return NotFound();

        DtoFactory.FromDto(entity, dto);

        await this.repository.CommitAsync();

        return Ok(entity.Id);
    }

    [HttpDelete("delete/{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] long id)
    {
        var rows = await repository.Query()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return rows == 0 ? NotFound() : NoContent();
    }

    [HttpPost("upsert-many")]
    public async Task UpsertMany([FromBody]IEnumerable<TDto> dtos)
    {
        var toUpdate = dtos.Where(x => x.Id > 0).ToList();
        var toInsert = dtos.Where(x => x.Id == 0).ToList();

        var ids = toUpdate.Select(x => x.Id).ToList();

        var existing = await repository.Query()
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();

        foreach (var dto in toUpdate)
        {
            var entity = existing.FirstOrDefault(e => e.Id == dto.Id);
            if (entity != null)
                DtoFactory.FromDto(entity, dto);
        }

        var newEntities = new List<TEntity>();

        foreach (var dto in toInsert)
        {
            var newEntity = repository.Create();
            DtoFactory.FromDto(newEntity, dto);
            newEntities.Add(newEntity);
        }

        await repository.InsertAsync(newEntities.ToArray());

        await repository.CommitAsync();
    }
}
