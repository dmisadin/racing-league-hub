using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Dto.GrandPrixDto;
using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;

namespace F1StatsServer.Repository
{
    public class SeasonRepository : GenericRepository<Season>, IGenericRepository<Season>, ISeasonRepository
    {
        private readonly AdventureContext _context;
        public SeasonRepository(AdventureContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<SeasonDisplayDto> GetSeasonData(int id)
        {
            var query = _context.Set<Season>()
                                .Where(c => c.Id == id)
                                .Select(e => new SeasonDisplayDto
                                {
                                    Name = e.Name,
                                    ImagePath = e.ImagePath,
                                    LapsRequiredPercentage = e.LapsRequiredPercentage,
                                    Game = MyMapper<GameDto, Game>.Map(_context.Set<Game>().Where(d => d.Id == e.GameId).FirstOrDefault()),
                                    Platform = MyMapper<PlatformDto, Platform>.Map(_context.Set<Platform>().Where(d => d.Id == e.PlatformId).FirstOrDefault()),
                                    QualPoints = MyMapper<SeasonPointDto, SeasonQualPoint>.MapList(e.SeasonQualPoints.ToList()),
                                    RacePoints = MyMapper<SeasonPointDto, SeasonRacePoint>.MapList(e.SeasonRacePoints.ToList()),
                                    SprintPoints = MyMapper<SeasonPointDto, SeasonSprintPoint>.MapList(e.SeasonSprintPoints.ToList()),
                                    LobbySetting = MyMapper<SeasonLobbySettingDto, SeasonLobbySetting>.Map(e.SeasonLobbySetting),
                                    Assist = MyMapper<SeasonAssistDto, SeasonAssist>.Map(e.SeasonAssist),
                                    GrandPrixes = MyMapper<GrandPrixStandardDto, GrandPrix>.MapList(e.GrandPrixes.ToList()),
                                    FastestLapPoint =MyMapper<SeasonPointDto, SeasonFastestLapPoint>.Map(e.SeasonFastestLapPoint),
                                });

            return query;
        }
    }
}
