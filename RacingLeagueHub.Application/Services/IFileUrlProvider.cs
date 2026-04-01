namespace RacingLeagueHub.Application.Services;

public interface IFileUrlProvider
{
    string GetFileUrl(Guid storageId, string extension);
    string BaseUrl { get; }
}