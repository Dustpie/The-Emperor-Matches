using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

/**
 * This class is used to create a token for the user
 * This token is used for authentication
 */
public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(User user)
    {
        var tokenKey = config["TokenKey"] ?? throw new("Cannot access tokenKey from app settings");
        if (tokenKey.Length < 64) throw new("The tokenKey needs to be longer");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserName)
                
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7), //Only for development purposes --> Security risk
            SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
    }
}