using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.ResourceService.Dtos;
using RacingLeagueHub.Application.Services.ResourceService.Persistence;
using RacingLeagueHub.Domain.Entities.Resources;

namespace RacingLeagueHub.Infrastructure.Persistence.Resources;

internal sealed class ResourceQueries : IResourceQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Resource, ResourceDto> mapper;

    public ResourceQueries(
        RacingContext racingContext,
        IDtoMapper<Resource, ResourceDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await racingContext.Resource
            .Where(r => r.Id == id)
            .Select(mapper.ToDtoExpression())
            .SingleOrDefaultAsync(ct);
    }

    public async Task<PagedResult<ResourceDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var query = racingContext.Resource
            .OrderByDescending(r => r.CreatedAt)
            .ThenByDescending(r => r.Id)
            .Select(mapper.ToDtoExpression());

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<ResourceDto>(
            items,
            page,
            pageSize,
            totalCount);
    }

    public async Task<string?> GetFileUrlAsync(long id, CancellationToken ct)
    {
        var resource = await racingContext.Resource
            .Where(r => r.Id == id)
            .Select(r => new
            {
                r.StorageId,
                r.Extension
            })
            .SingleOrDefaultAsync(ct);

        if (resource is null)
            return null;

        return BuildS3Key(resource.StorageId, resource.Extension);
    }

    private static string BuildS3Key(Guid storageId, string extension)
        => $"uploads/{storageId}.{extension}";
}