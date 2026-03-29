using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.API.DtoFactories;
using RacingLeagueHub.API.Dtos.Resource;
using RacingLeagueHub.BLL.Entities.Resources;
using RacingLeagueHub.BLL.Models;
using RacingLeagueHub.BLL.Services.Interfaces;
using RacingLeagueHub.Data.Repositories;

namespace RacingLeagueHub.BLL.Services;

public class ResourceService(
    IResourceRepository resourceRepository,
    IStorageService storageService) : IResourceService
{
    private IDtoFactory<Resource, ResourceDto> DtoFactory => new ResourceDtoFactory(storageService);

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

    public async Task<ResourceDto> UploadAsync(IFormFile file, bool? isThumbnail, CancellationToken ct = default)
    {
        var uid = Guid.NewGuid();
        var extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();
        var s3Key = BuildS3Key(uid, extension);

        // 1. Upload to S3 first — if this fails, nothing is saved to DB
        await using var stream = file.OpenReadStream();
        await storageService.UploadAsync(s3Key, stream, file.ContentType, ct);

        // 2. Save to DB as unconfirmed — confirmed when the parent form is submitted
        var resource = new Resource
        {
            StorageId = uid,
            FileName = file.FileName,
            Extension = extension,
            MimeType = file.ContentType,
            SizeInBytes = file.Length,
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