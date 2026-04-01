using Microsoft.Extensions.Options;

namespace RacingLeagueHub.Application.Storage;

public static class S3Settings
{
    public static S3Options Values { get; private set; } = new();

    public static void Initialize(S3Options options)
    {
        Values = options;
    }

    public static string? GetFileUrl(Guid storageId, string extension)
    {
        return Values.PublicBaseUrl + "/uploads/" + storageId + "." + extension;
    }
}