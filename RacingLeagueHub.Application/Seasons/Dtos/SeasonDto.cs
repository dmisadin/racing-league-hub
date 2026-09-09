using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Application.Seasons.Dtos;

public class SeasonDto : BaseDto
{
    public EncryptedId LeagueId { get; set; }
    public string Name { get; set; }
    public Platform Platform { get; set; }
    public Game Game { get; set; }
    public short LapPercentageRequired { get; set; }
    public string Slug { get; set; }
    public EncryptedId? LogoResourceId { get; set; }
    public string? LogoUrl { get; set; }
}

public class CreateSeasonDto
{
    public EncryptedId LeagueId { get; set; }
    public string Name { get; set; }
    public Platform Platform { get; set; }
    public Game Game { get; set; }
    public short LapPercentageRequired { get; set; }
    public string Slug { get; set; }
    public EncryptedId? LogoResourceId { get; set; }
}

public class UpdateSeasonDto
{
    public string Name { get; set; }
    public Platform Platform { get; set; }
    public Game Game { get; set; }
    public short LapPercentageRequired { get; set; }
    public string Slug { get; set; }
    public EncryptedId? LogoResourceId { get; set; }
}