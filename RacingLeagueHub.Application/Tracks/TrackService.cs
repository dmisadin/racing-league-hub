using RacingLeagueHub.Application.Common.Dtos;
using RacingLeagueHub.Application.Tracks.Dtos;
using RacingLeagueHub.Application.Tracks.Persistence;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.Tracks;

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

    public async Task<TrackDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await queries.GetByIdAsync(id, ct);
    }

    public async Task<PagedResult<TrackDto>> GetPagedAsync(int page, CancellationToken ct)
    {
        return await queries.GetPagedAsync(page, pageSize: 10, ct);
    }

    public async Task<List<LookupDto>> GetLookupsAsync(CancellationToken ct)
    {
        return await queries.GetLookupsAsync(ct);
    }

    public async Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct)
    {
        return await commands.AddAsync(dto, ct);
    }

    public async Task<TrackDto?> UpdateAsync(int id, UpdateTrackDto dto, CancellationToken ct)
    {
        return await commands.UpdateAsync(id, dto, ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        return await commands.DeleteAsync(id, ct);
    }
}
