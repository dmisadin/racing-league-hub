using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace RacingLeagueHub.Api.Middleware;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        var secret = config["Jwt:Secret"];

        if (string.IsNullOrWhiteSpace(issuer))
            throw new InvalidOperationException("Missing configuration value: Jwt:Issuer.");

        if (string.IsNullOrWhiteSpace(audience))
            throw new InvalidOperationException("Missing configuration value: Jwt:Audience.");

        if (string.IsNullOrWhiteSpace(secret))
            throw new InvalidOperationException("Missing configuration value: Jwt:Secret.");

        if (secret.Length < 32)
            throw new InvalidOperationException("Jwt:Secret must be at least 32 characters long.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidAlgorithms = [SecurityAlgorithms.HmacSha256],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secret)),

                    ClockSkew = TimeSpan.Zero,

                    NameClaimType = "sub",
                    RoleClaimType = "role"
                };
            });

        return services;
    }
}