namespace F1StatsServer.Dto
{
    public class RaceDto
    {
        public byte? Position { get; set; }
        public string? TeamName { get; set; }
        public string? DriverName { get; set; }
        public decimal? RaceTime { get; set; }
        public short? Penalty { get; set; }
        public byte? Points { get; set; }
        public string? Tyres { get; set; }
    }
}
