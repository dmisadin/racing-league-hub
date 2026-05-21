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
}