using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class GenericController<TEntity, TDto> : BaseController
    where TEntity : IEntity
    where TDto : BaseDto
{
    protected readonly IRepository<TEntity> repository;

    public GenericController(IRepository<TEntity> repository)
    {
        this.repository = repository;
    }

    protected abstract IDtoMapper<TEntity, TDto> DtoFactory { get; }


    [HttpGet("get-by-id/{id}")]
    public virtual async Task<ActionResult<TDto>> GetById(
        [FromRoute] EncryptedId id,
        CancellationToken ct = default)
    {
        var dto = await repository.GetByIdAsync<TDto>(
            id.RawId,
            DtoFactory.ToDtoExpression());

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpGet]
    [AllowAnonymous]
    public virtual async Task<ActionResult<PagedResult<TDto>>> GetPaged(
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await repository.GetPagedAsync(
            DtoFactory.ToDtoExpression(),
            page,
            pageSize: 10,
            ct);

        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<ActionResult<EncryptedId>> Create(
        [FromBody] TDto dto,
        CancellationToken ct = default)
    {
        var entity = repository.Create();

        DtoFactory.FromDto(entity, dto);

        await repository.InsertAsync(entity);
        await repository.CommitAsync(ct);

        return Ok(new EncryptedId(entity.Id));
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<EncryptedId>> Update(
        [FromRoute] EncryptedId id,
        [FromBody] TDto dto,
        CancellationToken ct = default)
    {
        if (id.RawId <= 0)
            return BadRequest("Invalid ID.");

        if (dto.Id is not null && dto.Id.RawId != id.RawId)
            return BadRequest("Route ID does not match body ID.");

        var entity = await repository.FindAsync(id.RawId);

        if (entity is null)
            return NotFound();

        DtoFactory.FromDto(entity, dto);

        await repository.CommitAsync(ct);

        return Ok(new EncryptedId(entity.Id));
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(
        [FromRoute] EncryptedId id,
        CancellationToken ct = default)
    {
        if (id.RawId <= 0)
            return BadRequest("Invalid ID.");

        var rows = await repository.ExecuteDeleteAsync(x => x.Id == id.RawId);

        return rows == 0
            ? NotFound()
            : NoContent();
    }

    /* Move to Application layer
    [HttpPost("upsert-many")]
    public async Task UpsertMany([FromBody]IEnumerable<TDto> dtos)
    {
        var toUpdate = dtos.Where(x => x.Id?.RawId > 0).ToList();
        var toInsert = dtos.Where(x => x.Id?.RawId == 0).ToList();

        var ids = toUpdate.Select(x => x.Id?.RawId).ToList();

        var existing = await repository.Query()
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();

        foreach (var dto in toUpdate)
        {
            var entity = existing.FirstOrDefault(e => e.Id == dto.Id?.RawId);
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
    */
}
