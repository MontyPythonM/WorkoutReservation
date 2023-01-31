using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JwtProvider(AuthenticationSettings authenticationSettings)
    {
        _authenticationSettings = authenticationSettings;
    }
    
    public string Generate(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };
        
        // if (user.ApplicationRoles.Any())
        // {
        //     claims.AddRange(user.ApplicationRoles.Select(role => new Claim(ClaimTypes.Role, role.Name.ToString())));
        // }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

        var jwtToken = new JwtSecurityToken(
            _authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtAudience,
            claims,
            null,
            expires,
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}