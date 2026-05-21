using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Dtos.Track;
using RacingLeagueHub.Domain.Abstractions.Admin;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class TrackLayoutRepository : GenericRepository<TrackLayout>, ITrackLayoutRepository
{
    public TrackLayoutRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public override async Task<long?> UpdateAsync<TDto>(
        Func<TrackLayout, TDto, bool> mappingFunction,
        long id,
        TDto dto,
        CancellationToken ct = default)
    {
        var entity = await this.Query()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (entity is null)
            return null;

        mappingFunction.Invoke(entity, dto);

        if (dto is TrackLayoutDto trackLayoutDto)
        {
            await SyncTrackLayoutGamesAsync(
                id,
                trackLayoutDto.TrackLayoutGames,
                ct);
        }

        await this.CommitAsync();

        return entity.Id;
    }

    private async Task SyncTrackLayoutGamesAsync(
        long trackLayoutId,
        IEnumerable<Game> desiredGames,
        CancellationToken ct = default)
    {
        var desiredSet = desiredGames
            .Distinct()
            .ToHashSet();

        var existingGames = await dbContext.Set<TrackLayoutGame>()
            .Where(x => x.TrackLayoutId == trackLayoutId)
            .Select(x => x.Game)
            .ToListAsync(ct);

        var existingSet = existingGames.ToHashSet();

        var gamesToAdd = desiredSet
            .Except(existingSet)
            .ToList();

        var gamesToRemove = existingSet
            .Except(desiredSet)
            .ToList();

        if (gamesToRemove.Count > 0)
        {
            await dbContext.Set<TrackLayoutGame>()
                .Where(x =>
                    x.TrackLayoutId == trackLayoutId
                    && gamesToRemove.Contains(x.Game))
                .ExecuteDeleteAsync(ct);
        }

        if (gamesToAdd.Count > 0)
        {
            var newRows = gamesToAdd.Select(game => new TrackLayoutGame
            {
                TrackLayoutId = trackLayoutId,
                Game = game
            });

            await dbContext.Set<TrackLayoutGame>().AddRangeAsync(newRows, ct);
        }
    }
}
