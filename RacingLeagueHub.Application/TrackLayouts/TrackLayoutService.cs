using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.TrackLayouts.Dtos;
using RacingLeagueHub.Application.TrackLayouts.Persistence;

namespace RacingLeagueHub.Application.TrackLayouts;

internal class TrackLayoutService : ITrackLayoutService
{
    private readonly ITrackLayoutQueries queries;
    private readonly ITrackLayoutCommands commands;

    public TrackLayoutService(
        ITrackLayoutQueries queries,
        ITrackLayoutCommands commands)
    {
        this.queries = queries;
        this.commands = commands;
    }

    public async Task<TrackLayoutDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await queries.GetByIdAsync(id, ct);
    }

    public async Task<PagedResult<TrackLayoutDto>> GetPagedAsync(int page, CancellationToken ct)
    {
        return await queries.GetPagedAsync(page, pageSize: 10, ct);
    }

    public async Task<TrackLayoutDto> AddAsync(CreateTrackLayoutDto dto, CancellationToken ct)
    {
        return await commands.AddAsync(dto, ct);
    }

    public async Task<TrackLayoutDto?> UpdateAsync(long id, UpdateTrackLayoutDto dto, CancellationToken ct)
    {
        return await commands.UpdateAsync(id, dto, ct);
    }
}
