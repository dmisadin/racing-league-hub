using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TrackService.Dtos;
using RacingLeagueHub.Application.Services.TrackService.Persistence;

namespace RacingLeagueHub.Application.Services.TrackService;

public class TrackService : ITrackService
{
    private readonly ITrackQueries queries;
    private readonly ITrackCommands commands;

    public TrackService(
        ITrackQueries queries,
        ITrackCommands commands)
    {
        this.queries = queries;
        this.commands = commands;
    }

    public async Task<TrackDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await queries.GetByIdAsync(id, ct);
    }

    public async Task<PagedResult<TrackDto>> GetPagedAsync(int page, CancellationToken ct)
    {
        return await queries.GetPagedAsync(page, pageSize: 10, ct);
    }

    public async Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct)
    {
        return await commands.AddAsync(dto, ct);
    }

    public async Task<TrackDto?> UpdateAsync(long id, UpdateTrackDto dto, CancellationToken ct)
    {
        return await commands.UpdateAsync(id, dto, ct);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        return await commands.DeleteAsync(id, ct);
    }
}
