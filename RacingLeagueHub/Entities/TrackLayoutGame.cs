using F1StatsServer.Entities.Enums;

namespace F1StatsServer.Entities
{
    public class TrackLayoutGame
    {
        public long TrackLayoutId { get; set; }
        public Game Game { get; set; }

        public virtual TrackLayout TrackLayout { get; set; }
    }
}
