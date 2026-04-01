namespace RacingLeagueHub.Domain.Services.Interfaces;

public interface IStorageService
{
    Task UploadAsync(string s3Key, Stream fileStream, string mimeType, CancellationToken ct = default);
    string GetBaseUrl();
    string GetFileUrl(string s3Key);
    Task DeleteAsync(string s3Key, CancellationToken ct = default);
}