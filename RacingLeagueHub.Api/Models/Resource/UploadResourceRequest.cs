namespace RacingLeagueHub.Api.Models.Resource;

public class UploadResourceRequest
{
    public IFormFile File { get; set; } = null!;
    public bool? IsThumbnail { get; set; }
}
