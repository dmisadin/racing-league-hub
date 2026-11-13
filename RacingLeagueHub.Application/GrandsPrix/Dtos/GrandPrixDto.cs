using RacingLeagueHub.Application.Common.Dtos;

namespace RacingLeagueHub.Application.GrandsPrix.Dtos;

public class GrandPrixDto : BaseDto
{
    public int TrackLayoutId { get; set; }
    public int? LeagueId { get; set; }
    public int SeasonId { get; set; }
    public int SeasonDivisionId { get; set; }
    public string DivisionSlug { get; set; }
    public string DivisionName { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }
    public string? TrackName { get; set; }
    public string? TrackCity { get; set; }
    public string? TrackCountryCode { get; set; }
}

public class CreateGrandPrixDto
{
    public int TrackLayoutId { get; set; }
    public int SeasonDivisionId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }
}

public class UpdateGrandPrixDto
{
    public int TrackLayoutId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }
}
