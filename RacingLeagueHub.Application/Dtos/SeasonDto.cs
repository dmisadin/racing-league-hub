using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Application.Dtos;

public class SeasonDto : BaseDto
{
    public string Name { get; set; }
    public Platform Platform { get; set; }
    public Game Game { get; set; }
    public short LapPercentageRequired { get; set; }
    public string Slug { get; set; }
    public EncryptedId? LogoResourceId { get; set; }
    public string? LogoUrl { get; set; }
}
