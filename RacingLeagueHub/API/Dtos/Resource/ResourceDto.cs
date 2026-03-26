namespace RacingLeagueHub.API.Dtos.Resource;

public class ResourceDto : ResourceBaseDto
{
    public Guid StorageId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool? IsThumbnail { get; set; }
}
