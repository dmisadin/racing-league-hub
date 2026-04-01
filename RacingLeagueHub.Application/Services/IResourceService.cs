using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Models.Resource;

namespace RacingLeagueHub.Application.Services;

public interface IResourceService
{
    Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken ct = default);
    Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct = default);
    Task ConfirmAsync(long uid, CancellationToken ct = default);
    Task DeleteAsync(long uid, CancellationToken ct = default);
    Task<string?> GetFileUrl(long id, CancellationToken ct = default);
}

