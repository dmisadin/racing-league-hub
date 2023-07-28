using F1StatsServer.Dto.UserDto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace F1StatsServer.Service
{

    //Possible change would be to make method CreateToken() private and invoked through an instance of this class
    public class TokenService
    {
        public static string CreateToken(UserStandardDto request, IConfiguration configuration, bool isAdmin)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, request.Email),
            };

            if (isAdmin) { claims.Add(new Claim(ClaimTypes.Role, "Admin")); }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
