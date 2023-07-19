using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace HubPoint.Services.Identity.Api;

public class TokenService
{
    public string GenerateToken()
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecurityKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var expirationTimeStamp = DateTime.Now.AddMinutes(5);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, "andrei.popescu"),
            new(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString())
        };

        var tokenOptions = new JwtSecurityToken(issuer: "HubPoint", claims: claims, expires: expirationTimeStamp,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}