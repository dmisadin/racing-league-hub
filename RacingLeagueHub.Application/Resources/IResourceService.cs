using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Resources.Dtos;

namespace RacingLeagueHub.Application.Resources;

public interface IResourceService
{
    Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<PagedResult<ResourceDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct = default);
    Task ConfirmAsync(long id, CancellationToken ct = default);
    Task DeleteAsync(long uid, CancellationToken ct = default);
    Task<string?> GetFileUrlAsync(long id, CancellationToken ct = default);
}