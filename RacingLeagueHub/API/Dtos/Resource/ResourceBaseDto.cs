using RacingLeagueHub.BLL.Models.Dtos;

namespace RacingLeagueHub.API.Dtos.Resource;

public class ResourceBaseDto : BaseDto
{
    public string FileUrl { get; set; }
    public string Extension { get; set; }
}
