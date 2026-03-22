namespace RacingLeagueHub.API.Dtos.Resource;

public class ResourceDto
{
    public Guid Uid { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool? IsThumbnail { get; set; }
    public string FileUrl { get; set; } = string.Empty;
}
