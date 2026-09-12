using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.LeagueUsers.Dtos;

public record LeagueUserRolesDto (long UserId, long LeagueId, string LeagueSlug, bool IsOwner, bool IsAdmin, bool IsEditor, bool IsSteward);