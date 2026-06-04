using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using RacingLeagueHub.Domain.Services.Interfaces;
using RacingLeagueHub.Infrastructure.Configuration;

namespace RacingLeagueHub.Infrastructure.Services;

internal class S3StorageService(IAmazonS3 s3Client, IOptions<S3Options> options) : IStorageService
{
    private readonly S3Options options = options.Value;

    public async Task UploadAsync(string s3Key, Stream fileStream, string mimeType, CancellationToken ct = default)
    {
        var request = new PutObjectRequest
        {
            BucketName = options.BucketName,
            Key = s3Key,
            InputStream = fileStream,
            ContentType = mimeType,
            AutoCloseStream = false
        };

        await s3Client.PutObjectAsync(request, ct);
    }

    public string GetBaseUrl() => options.PublicBaseUrl.TrimEnd('/');

    public string GetFileUrl(string s3Key)
    {
        return $"{options.PublicBaseUrl.TrimEnd('/')}/{s3Key.TrimStart('/')}";
    }

    public async Task DeleteAsync(string s3Key, CancellationToken ct = default)
    {
        await s3Client.DeleteObjectAsync(options.BucketName, s3Key, ct);
    }
}
