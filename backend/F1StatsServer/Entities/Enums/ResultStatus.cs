using System.ComponentModel;

namespace F1StatsServer.Entities.Enums
{
    public enum ResultStatus : byte
    {
        [Description("Invalid")]
        Invalid = 0,
        [Description("Inactive")]
        Inactive,
        [Description("Active")]
        Active,
        [Description("Finished")]
        Finished,
        [Description("Did not finish")]
        DNF,
        [Description("Disqualified")]
        DSQ,
        [Description("Not classified")]
        NC,
        [Description("Retired")]
        Retired
    }
}
