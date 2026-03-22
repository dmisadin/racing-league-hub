namespace RacingLeagueHub.BLL.Models.Storage;

public class S3Options
{
    public string BucketName { get; set; } = string.Empty;
    public string PublicBaseUrl { get; set; } = string.Empty;
    public int PresignedUrlExpiryMinutes { get; set; } = 15;
}
