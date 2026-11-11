using RacingLeagueHub.Application.Common.Dtos;

namespace RacingLeagueHub.Application.Divisions.Dtos;

public class DivisionDto : BaseDto
{
    public int LeagueId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public short Tier { get; set; }
}
