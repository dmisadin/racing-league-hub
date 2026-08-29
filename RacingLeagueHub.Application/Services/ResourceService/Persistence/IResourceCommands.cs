using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Services.ResourceService.Dtos;

namespace RacingLeagueHub.Application.Services.ResourceService.Persistence;

public interface IResourceCommands
{
    Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct);
    Task<bool> ConfirmAsync(long id, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
