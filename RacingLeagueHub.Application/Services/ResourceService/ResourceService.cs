using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Services.ResourceService.Dtos;
using RacingLeagueHub.Application.Services.ResourceService.Persistence;
using RacingLeagueHub.Domain.Services.Interfaces;

namespace RacingLeagueHub.Application.Services.ResourceService;

public class ResourceService : IResourceService
{
    private readonly IResourceQueries queries;
    private readonly IResourceCommands commands;
    private readonly IStorageService storageService;

    public ResourceService(
        IResourceQueries queries,
        IResourceCommands commands,
        IStorageService storageService)
    {
        this.queries = queries;
        this.commands = commands;
        this.storageService = storageService;
    }

    public Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct = default)
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

    public async Task ConfirmAsync(long id, CancellationToken ct = default)
    {
        var confirmed = await commands.ConfirmAsync(id, ct);

        if (!confirmed)
            throw new KeyNotFoundException(
                $"Resource {id} not found.");
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        var deleted = await commands.DeleteAsync(id, ct);

        if (!deleted)
            throw new KeyNotFoundException(
                $"Resource {id} not found.");
    }

    public Task<string?> GetFileUrlAsync(long id, CancellationToken ct = default)
    {
        return queries.GetFileUrlAsync(id, ct);
    }
}