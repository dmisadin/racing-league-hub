using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Application.Dtos;

public class LeagueDto : BaseDto
{
    public Region Region { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
    public string Timezone { get; set; }
    public string Slug { get; set; }
    public EncryptedId? LogoResourceId { get; set; }
    public string? LogoUrl { get; set; }
}
