using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.GrandsPrix.Dtos;

public class GrandPrixDto : BaseDto
{
    public EncryptedId TrackLayoutId { get; set; }
    public EncryptedId? LeagueId { get; set; }
    public EncryptedId SeasonId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }
}

public class CreateGrandPrixDto
{
    public EncryptedId TrackLayoutId { get; set; }
    public EncryptedId SeasonId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }
}

public class UpdateGrandPrixDto
{
    public EncryptedId TrackLayoutId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }
}
