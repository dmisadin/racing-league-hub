using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Dtos.User;

public record UserLeagueRolesDto (EncryptedId LeagueId, string LeagueSlug, bool IsOwner, bool IsAdmin, bool IsEditor, bool IsSteward);