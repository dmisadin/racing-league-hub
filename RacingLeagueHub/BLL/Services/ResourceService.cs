using RacingLeagueHub.API.Dtos.Resource;
using RacingLeagueHub.BLL.Entities.Resources;
using RacingLeagueHub.BLL.Services.Interfaces;
using RacingLeagueHub.Data.Repositories;

namespace RacingLeagueHub.BLL.Services;

public class ResourceService(
    IResourceRepository resourceRepository,
    IStorageService storageService) : IResourceService
{
    public async Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken ct = default)
    {
        var resources = await resourceRepository.GetAllAsync(ct);
        return resources.Select(ToDto).ToList();
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
            Uid = uid,
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

    public async Task ConfirmAsync(Guid uid, CancellationToken ct = default)
    {
        var resource = await GetOrThrowAsync(uid, ct);
        await resourceRepository.ConfirmAsync(resource, ct);
    }

    public async Task DeleteAsync(Guid uid, CancellationToken ct = default)
    {
        var resource = await GetOrThrowAsync(uid, ct);
        var s3Key = BuildS3Key(resource.Uid, resource.Extension);

        await storageService.DeleteAsync(s3Key, ct);
        await resourceRepository.DeleteAsync(resource, ct);
    }

    private async Task<Resource> GetOrThrowAsync(Guid uid, CancellationToken ct)
    {
        var resource = await resourceRepository.GetOneAsync(uid, ct);
        if (resource is null)
            throw new KeyNotFoundException($"Resource {uid} not found.");
        return resource;
    }

    private static string BuildS3Key(Guid uid, string extension)
        => $"uploads/{uid}.{extension}";

    private ResourceDto ToDto(Resource r) => new()
    {
        Uid = r.Uid,
        FileName = r.FileName,
        Extension = r.Extension,
        MimeType = r.MimeType,
        SizeInBytes = r.SizeInBytes,
        CreatedAt = r.CreatedAt,
        IsThumbnail = r.IsThumbnail,
        FileUrl = storageService.GetFileUrl(BuildS3Key(r.Uid, r.Extension))
    };
}