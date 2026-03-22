namespace RacingLeagueHub.BLL.Entities.Resources;

public class Resource : EntityBase
{
    public Guid Uid { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string MimeType { get; set; }
    public long SizeInBytes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool? IsThumbnail { get; set; }
    public ResourceStatus Status { get; set; }
}
