using System.ComponentModel;

namespace RacingLeagueHub.Domain.Common.Models.Enums
{
    public enum Region : short
    {
        [Description("Adria")]
        Adria = 1,
        [Description("Europe")]
        Europe,
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
