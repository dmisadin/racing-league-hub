using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Dtos.GrandPrix;

public class GrandPrixDto : BaseDto
{
    public EncryptedId TrackLayoutId { get; set; }
    public EncryptedId LeagueId { get; set; }
    public EncryptedId SeasonId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }
}
