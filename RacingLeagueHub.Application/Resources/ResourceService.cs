using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Resources.Dtos;
using RacingLeagueHub.Application.Resources.Persistence;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.Resources;

public class ResourceService : IResourceService
{
    private readonly IResourceQueries queries;
    private readonly IResourceCommands commands;

    public ResourceService(
        IResourceQueries queries,
        IResourceCommands commands)
    {
        this.queries = queries;
        this.commands = commands;
    }

    public Task<ResourceDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return queries.GetByIdAsync(id, ct);
    }

    public Task<PagedResult<ResourceDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        return queries.GetPagedAsync(
            page,
            pageSize,
            ct);
    }

    public Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct = default)
    {
        return commands.UploadAsync(file, isThumbnail, ct);
    }

    public async Task ConfirmAsync(int id, CancellationToken ct = default)
    {
        var confirmed = await commands.ConfirmAsync(id, ct);

        if (!confirmed)
            throw new KeyNotFoundException(
                $"Resource {id} not found.");
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var deleted = await commands.DeleteAsync(id, ct);

        if (!deleted)
            throw new KeyNotFoundException(
                $"Resource {id} not found.");
    }

    public Task<string?> GetFileUrlAsync(int id, CancellationToken ct = default)
    {
        return queries.GetFileUrlAsync(id, ct);
    }
}