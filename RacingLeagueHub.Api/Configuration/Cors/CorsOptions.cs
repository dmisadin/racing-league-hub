namespace RacingLeagueHub.Api.Configuration.Cors;

public sealed class CorsOptions
{
    public string[] AllowedOrigins { get; init; } = [];
    public string[] AllowedMethods { get; init; } = [];
    public string[] AllowedHeaders { get; init; } = [];
    public bool AllowCredentials { get; init; }
}
