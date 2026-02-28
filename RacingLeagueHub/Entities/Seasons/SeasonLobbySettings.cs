using F1StatsServer.Entities.Enums.Lobby;

namespace F1StatsServer.Entities.Seasons;

public class SeasonLobbySettings : EntityBase
{
    public long SeasonId { get; set; }
    public QualifyingType QualifyingType { get; set; }
    public RaceDistance RaceDistancePercentage { get; set; }
    public bool FormationLap { get; set; }
    public Weather Weather { get; set; }
    public CornerCuttingStringency CornerCutting { get; set; }
    public CarDamage CarDamage { get; set; }
    public CarDamageRate CarDamageRate { get; set; }
    public bool ParcFerme { get; set; }
    public bool EqualCarPerformance { get; set; }
    public SafetyCar SafetyCar { get; set; }
    public bool Collisions { get; set; }
    public bool Ghosting { get; set; }
    public RaceStart Start { get; set; }

    public virtual Season Season { get; set; }
}
