using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.Data;
using RacingLeagueHub.Data.Repositories;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class TrackLayoutRepository : GenericRepository<TrackLayout>
{
    public TrackLayoutRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public override async Task<long?> UpdateAsync<TDto>(Func<TrackLayout, TDto, bool> mappingFunction, long id, TDto dto)
    {

        var entity = await this.Query().Include(e => e.TrackLayoutGames)
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();

        if (entity == null)
            return null;

        mappingFunction.Invoke(entity, dto);

        await this.CommitAsync();

        return entity.Id;
    }
}
