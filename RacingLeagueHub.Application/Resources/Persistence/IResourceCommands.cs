using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Resources.Dtos;

namespace RacingLeagueHub.Application.Resources.Persistence;

public interface IResourceCommands
{
    Task<ResourceDto> UploadAsync(FileUploadRequest file, bool? isThumbnail, CancellationToken ct);
    Task<bool> ConfirmAsync(int id, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
