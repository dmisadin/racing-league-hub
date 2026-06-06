using System.ComponentModel.DataAnnotations;

namespace RacingLeagueHub.Infrastructure.Configuration;

internal class S3Options
{
    public const string SectionName = "S3";

    [Required]
    public string BucketName { get; init; } = string.Empty;

    [Required]
    public string PublicBaseUrl { get; init; } = string.Empty;

    [Range(1, 1440)]
    public int PresignedUrlExpiryMinutes { get; init; } = 15;
}
