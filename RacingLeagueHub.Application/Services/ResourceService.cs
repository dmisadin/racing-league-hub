using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Domain.Entities.Resources;
using RacingLeagueHub.Domain.Infrastructure;
using RacingLeagueHub.Domain.Services.Interfaces;

namespace RacingLeagueHub.Application.Services;

public class ResourceService(
    IResourceRepository resourceRepository,
    IStorageService storageService) : IResourceService
{
    private IDtoMapper<Resource, ResourceDto> DtoFactory => new ResourceDtoFactory(storageService);

    public async Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        return await resourceRepository.Query()
            .Where(r => r.Id == id)
            .Select(DtoFactory.ToDtoExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken ct = default)
    {
        return resourceRepository.Query()
            .Select(DtoFactory.ToDtoExpression())
            .ToList();
    }

    public async Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct = default)
    {
        var uid = Guid.NewGuid();
        var extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();
        var s3Key = BuildS3Key(uid, extension);

        await storageService.UploadAsync(s3Key, file.Content, file.ContentType, ct);

        var resource = new Resource
        {
            StorageId = uid,
            FileName = file.FileName,
            Extension = extension,
            MimeType = file.ContentType,
            SizeInBytes = file.SizeInBytes,
            IsThumbnail = isThumbnail,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = ResourceStatus.Pending
        };

        await resourceRepository.CreateAsync(resource, ct);
        return ToDto(resource);
    }

    public async Task ConfirmAsync(long id, CancellationToken ct = default)
    {
        var resource = await GetOrThrowAsync(id, ct);
        await resourceRepository.ConfirmAsync(resource, ct);
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        var resource = await GetOrThrowAsync(id, ct);
        var s3Key = BuildS3Key(resource.StorageId, resource.Extension);

        await storageService.DeleteAsync(s3Key, ct);
        await resourceRepository.DeleteAsync(resource, ct);
    }

    public async Task<string?> GetFileUrl(long id, CancellationToken ct = default)
    {
        var resource = await resourceRepository.GetOneAsync(id, ct);
        if (resource == null)
            return string.Empty;

        return storageService.GetFileUrl(BuildS3Key(resource.StorageId, resource.Extension));
    }

    private async Task<Resource> GetOrThrowAsync(long id, CancellationToken ct)
    {
        var resource = await resourceRepository.GetOneAsync(id, ct);
        if (resource is null)
            throw new KeyNotFoundException($"Resource {id} not found.");
        return resource;
    }

    private static string BuildS3Key(Guid uid, string extension)
        => $"uploads/{uid}.{extension}";

    private ResourceDto ToDto(Resource r) => new()
    {
        Id = new EncryptedId(r.Id),
        StorageId = r.StorageId,
        FileName = r.FileName,
        Extension = r.Extension,
        MimeType = r.MimeType,
        SizeInBytes = r.SizeInBytes,
        CreatedAt = r.CreatedAt,
        IsThumbnail = r.IsThumbnail,
        FileUrl = storageService.GetFileUrl(BuildS3Key(r.StorageId, r.Extension))
    };
}