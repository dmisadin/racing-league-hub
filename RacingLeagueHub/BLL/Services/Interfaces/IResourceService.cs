using RacingLeagueHub.API.Dtos.Resource;

namespace RacingLeagueHub.BLL.Services.Interfaces;

public interface IResourceService
{
    Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken ct = default);
    Task<ResourceDto> UploadAsync(IFormFile file, bool? isThumbnail, CancellationToken ct = default);
    Task ConfirmAsync(long uid, CancellationToken ct = default);
    Task DeleteAsync(long uid, CancellationToken ct = default);
    Task<string?> GetFileUrl(long id, CancellationToken ct = default);
}

