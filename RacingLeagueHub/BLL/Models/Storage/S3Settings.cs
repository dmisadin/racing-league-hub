namespace RacingLeagueHub.BLL.Models.Storage;

public class S3Settings
{
    public static S3Options Values { get; private set; } = new();

    public static void Initialize(IConfiguration configuration)
    {
        configuration.Bind(Values);
    }

    public static string? GetFileUrl(Guid storageId, string extension)
    {
        return Values.PublicBaseUrl + "/uploads/" + storageId + "." + extension;
    }
}
