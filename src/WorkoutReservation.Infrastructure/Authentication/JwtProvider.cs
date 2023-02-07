using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IPermissionService _permissionService;
    
    public JwtProvider(AuthenticationSettings authenticationSettings, IPermissionService permissionService)
    {
        _authenticationSettings = authenticationSettings;
        _permissionService = permissionService;
    }
    
    public async Task<string> GenerateAsync(ApplicationUser user, CancellationToken token)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}")
        };

        var permissions = await _permissionService.GetPermissionsAsync(user.Id, token);
        claims.AddRange(permissions.Select(permission => new Claim(CustomClaims.Permissions, permission)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

        var jwt = new JwtSecurityToken(
            _authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtAudience,
            claims,
            null,
            expires,
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}