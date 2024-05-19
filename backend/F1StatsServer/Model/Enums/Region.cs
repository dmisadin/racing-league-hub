using System.ComponentModel;

namespace F1StatsServer.Model.Enums
{
    public enum Region
    {
        [Description("Europe")]
        Europe = 1,
        [Description("North America")]
        NorthAmerica,
        [Description("South America")]
        SouthAmerica,
        [Description("Asia")]
        Asia,
        [Description("Oceania")]
        Oceania,
        [Description("Africa")]
        Africa
    }
}
