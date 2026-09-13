namespace RacingLeagueHub.Domain.Entities;

public class DcCountry : EntityBase
{
    public int Id { get; set; }
    public string CodeAlpha2 { get; set; }
    public string CodeAlpha3 { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}