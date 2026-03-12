using RacingLeagueHub.BLL.Models.Enums;

namespace RacingLeagueHub.Entities
{
    public class TrackLayoutGame
    {
        public long TrackLayoutId { get; set; }
        public Game Game { get; set; }

        public virtual TrackLayout TrackLayout { get; set; }
    }
}
