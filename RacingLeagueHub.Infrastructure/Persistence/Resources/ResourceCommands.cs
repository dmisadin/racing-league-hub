using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Services.ResourceService.Dtos;
using RacingLeagueHub.Application.Services.ResourceService.Persistence;
using RacingLeagueHub.Domain.Entities.Resources;
using RacingLeagueHub.Domain.Services.Interfaces;

namespace RacingLeagueHub.Infrastructure.Persistence.Resources;

internal sealed class ResourceCommands : IResourceCommands
{
    private readonly RacingContext racingContext;
    private readonly IStorageService storageService;
    private readonly IDtoMapper<Resource, ResourceDto> mapper;

    public ResourceCommands(
        RacingContext racingContext,
        IStorageService storageService,
        IDtoMapper<Resource, ResourceDto> mapper)
    {
        this.racingContext = racingContext;
        this.storageService = storageService;
        this.mapper = mapper;
    }

    public async Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct)
    {
        var storageId = Guid.NewGuid();

        var extension = Path.GetExtension(file.FileName)
            .TrimStart('.')
            .ToLowerInvariant();

        var s3Key = BuildS3Key(storageId, extension);

        await storageService.UploadAsync(
            s3Key,
            file.Content,
            file.ContentType,
            ct);

        var resource = new Resource
        {
            StorageId = storageId,
            FileName = file.FileName,
            Extension = extension,
            MimeType = file.ContentType,
            SizeInBytes = file.SizeInBytes,
            IsThumbnail = isThumbnail,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = ResourceStatus.Pending
        };

        racingContext.Resource.Add(resource);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(resource);
    }

    public async Task<bool> ConfirmAsync(long id, CancellationToken ct)
    {
        var resource = await racingContext.Resource
            .SingleOrDefaultAsync(
                r => r.Id == id,
                ct);

        if (resource is null)
            return false;

        resource.Status = ResourceStatus.Active;

        await racingContext.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var resource = await racingContext.Resource
            .SingleOrDefaultAsync(r => r.Id == id, ct);

        if (resource is null)
            return false;

        var s3Key = BuildS3Key(resource.StorageId, resource.Extension);

        await storageService.DeleteAsync(s3Key, ct);

        racingContext.Resource.Remove(resource);

        await racingContext.SaveChangesAsync(ct);

        return true;
    }

    private static string BuildS3Key(Guid storageId, string extension)
        => $"uploads/{storageId}.{extension}";
}