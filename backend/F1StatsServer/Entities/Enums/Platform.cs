using System.ComponentModel;

namespace F1StatsServer.Entities.Enums
{
    public enum Platform
    {
        [Description("Steam")]
        Steam = 1,
        [Description("PlayStation")]
        PlayStation = 2,
        [Description("Xbox")]
        Xbox = 3,
        [Description("EA")]
        Origin = 4,
        [Description("Crossplay")]
        Crossplay = 5,
    }
}
