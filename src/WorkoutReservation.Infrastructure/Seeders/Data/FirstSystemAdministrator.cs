using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class FirstSystemAdministrator : IFirstSystemAdministrator
{
    private readonly IApplicationRoleRepository _roleRepository;
    
    public FirstSystemAdministrator(IApplicationRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    
    public async Task<ApplicationUser> Create(SystemAdministratorSettings settings, 
        IPasswordHasher<ApplicationUser> passwordHasher, CancellationToken token)
    {
        var adminUser = new ApplicationUser(settings.Email, settings.FirstName, settings.LastName, Gender.Unspecified, null, "");
        var hashPassword = passwordHasher.HashPassword(adminUser, settings.Password);
        
        adminUser.SetPasswordHash(hashPassword);

        var role = await _roleRepository.GetAsync(Role.SystemAdministrator, token);
        adminUser.SetRole(role);
        
        return adminUser;
    }
}
