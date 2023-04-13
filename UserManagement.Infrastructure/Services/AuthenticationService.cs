using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Contracts;

namespace UserManagement.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly string _jwtSecret;
    private readonly int _jwtExpirationInMinutes;

    public AuthenticationService(string jwtSecret, int jwtExpirationInMinutes)
    {
        _jwtSecret = jwtSecret;
        _jwtExpirationInMinutes = jwtExpirationInMinutes;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email.Value),
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationInMinutes),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}