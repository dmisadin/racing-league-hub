using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Application.Services.GameTeamService.Dtos;

public class GameTeamDto : BaseDto
{
    public Game Game { get; init; }
    public EncryptedId TeamId { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; }
    public string Abbreviation { get; init; }
    public string? Color { get; init; }
    public short TelemetryId { get; init; }
    public EncryptedId? LogoResourceId { get; init; }
    public string? LogoUrl { get; init; }
}

public sealed class CreateGameTeamDto
{
    public Game Game { get; init; }
    public EncryptedId TeamId { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; }
    public string Abbreviation { get; init; }
    public string? Color { get; init; }
    public short TelemetryId { get; init; }
    public EncryptedId? LogoResourceId { get; init; }
}

public sealed class UpdateGameTeamDto
{
    public Game Game { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; }
    public string Abbreviation { get; init; }
    public string? Color { get; init; }
    public short TelemetryId { get; init; }
    public EncryptedId? LogoResourceId { get; init; }
}