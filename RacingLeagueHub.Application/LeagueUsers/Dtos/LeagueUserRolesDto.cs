using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.LeagueUsers.Dtos;

public record LeagueUserRolesDto (int UserId, int LeagueId, string LeagueSlug, bool IsOwner, bool IsAdmin, bool IsEditor, bool IsSteward);