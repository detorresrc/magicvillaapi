using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MagicVilla.Models;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    private readonly ILogger<TokenService> _logger;

    public TokenService(IConfiguration config, ILogger<TokenService> logger)
    {
        _logger = logger;
        _config = config;
    }

    public string CreateToken(LocalUser user)
    {
        var claims = new List<Claim>{
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(
            tokenHandler.CreateToken(tokenDescriptor)
        );
    }
}