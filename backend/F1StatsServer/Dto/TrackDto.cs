namespace F1StatsServer.Dto
{
    public class TrackDto
    {
        public string? TrackName { get; set; }
        public byte? Turns { get; set; }
        public byte? LeftTurns { get; set; }
        public decimal? Elevation { get; set; }
        public short? Length { get; set; }
    }
}
