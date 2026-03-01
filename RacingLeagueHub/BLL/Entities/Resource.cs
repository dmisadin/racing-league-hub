namespace RacingLeagueHub.Entities;

public class Resource : EntityBase
{
    public string Url { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string MimeType { get; set; }
    public long SizeInBytes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool? IsThumbnail { get; set; }
}
