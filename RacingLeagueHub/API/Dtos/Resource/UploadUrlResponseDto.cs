namespace RacingLeagueHub.API.Dtos.Resource;

public class UploadUrlResponseDto
{
    public UploadUrlResponseDto(Guid uid, string uploadUrl)
    {
        this.Uid = uid;
        this.UploadUrl = uploadUrl;
    }

    public Guid Uid { get; set; }
    public string UploadUrl { get; set; } = string.Empty;
}