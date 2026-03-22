namespace RacingLeagueHub.BLL.Services.Interfaces;

public interface IStorageService
{
    /// <summary>
    /// Uploads a file stream directly to S3 from the server.
    /// </summary>
    Task UploadAsync(string s3Key, Stream fileStream, string mimeType, CancellationToken ct = default);

    /// <summary>
    /// Returns the public URL to access a stored file.
    /// </summary>
    string GetFileUrl(string s3Key);

    /// <summary>
    /// Deletes a file from storage.
    /// </summary>
    Task DeleteAsync(string s3Key, CancellationToken ct = default);
}