using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Resources.Dtos;

namespace RacingLeagueHub.Application.Resources.Persistence;

public interface IResourceCommands
{
    Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct);
    Task<bool> ConfirmAsync(long id, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
