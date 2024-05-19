using System.ComponentModel;

namespace F1StatsServer.Entities.Enums
{
    public enum Platform
    {
        [Description("Steam")]
        Steam = 1,
        [Description("PlayStation")]
        PlayStation = 3,
        [Description("Xbox")]
        Xbox = 4,
        [Description("EA")]
        Origin = 6,
    }
}
