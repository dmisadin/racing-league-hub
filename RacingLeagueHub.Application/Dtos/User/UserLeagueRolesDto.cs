using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Dtos.User;

public record UserLeagueRolesDto (EncryptedId LeagueId, bool IsOwner, bool IsAdmin, bool IsEditor, bool IsSteward);