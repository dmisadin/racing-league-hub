using RacingLeagueHub.API.Dtos.Resource;

namespace RacingLeagueHub.BLL.Services.Interfaces;

public interface IResourceService
{
    Task<IReadOnlyList<ResourceDto>> GetAllAsync(CancellationToken ct = default);
    Task<ResourceDto> UploadAsync(IFormFile file, bool? isThumbnail, CancellationToken ct = default);
    Task ConfirmAsync(Guid uid, CancellationToken ct = default);
    Task DeleteAsync(Guid uid, CancellationToken ct = default);
}

