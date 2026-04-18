using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Dtos;

public class LookupDto
{
    public EncryptedId Id { get; set; }
    public string Label { get; set; }
}
