namespace RacingLeagueHub.Application.Dtos.Resource;

public class ResourceDto : BaseDto
{
    public Guid StorageId { get; set; }
    public string FileUrl { get; set; }
    public string Extension { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool? IsThumbnail { get; set; }
}
