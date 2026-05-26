using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Services.Identity;

public class JwtService(IConfiguration config) : IJwtService
{
    private readonly string _secret = config["Jwt:Secret"]!;
    private readonly string _issuer = config["Jwt:Issuer"]!;
    private readonly string _audience = config["Jwt:Audience"]!;
    private readonly int _expiryMinutes = int.Parse(config["Jwt:ExpiryMinutes"] ?? "15");

    public string GenerateAccessToken(User user)
    {
        var userEncryptedId = new EncryptedId(user.Id);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userEncryptedId.EncryptedReadonlyValue),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("username", user.Username),
            new("role", user.IsAdmin ? "Admin" : "User")
        };

        if (user.DriverId.HasValue)
            claims.Add(new("driverId", user.DriverId.Value.ToString()));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: GetAccessTokenExpiry(),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    public DateTime GetAccessTokenExpiry() =>
        DateTime.UtcNow.AddMinutes(_expiryMinutes);

    public string GenerateTwoFactorToken(User user)
    {
        var encryptedUserId = new EncryptedId(user.Id).EncryptedReadonlyValue;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, encryptedUserId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim("purpose", "2fa-login")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_secret)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateTwoFactorToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler
        { 
            MapInboundClaims = false 
        };

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _issuer,

            ValidateAudience = true,
            ValidAudience = _audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_secret)
            ),

            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },

            NameClaimType = JwtRegisteredClaimNames.Sub,
            RoleClaimType = ClaimTypes.Role
        };

        try
        {
            var principal = tokenHandler.ValidateToken(
                token,
                validationParameters,
                out var validatedToken
            );

            if (validatedToken is not JwtSecurityToken jwtToken)
                return null;

            if (!jwtToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var purpose = principal.FindFirst("purpose")?.Value;

            if (purpose != "2fa-login")
                return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }

    public long GetUserIdFromPrincipal(ClaimsPrincipal principal)
    {
        var encryptedSub =
            principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? principal.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(encryptedSub))
            throw new UnauthorizedAccessException("Missing user id claim.");

        return new EncryptedId(encryptedSub).RawId;
    }
}