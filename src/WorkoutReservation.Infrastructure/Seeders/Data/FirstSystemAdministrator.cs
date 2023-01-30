using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal static class FirstSystemAdministrator
{
    public static ApplicationUser Create(SystemAdministratorSettings settings, 
        IPasswordHasher<ApplicationUser> passwordHasher)
    {
        var adminUser = new ApplicationUser(settings.Email, settings.FirstName, settings.LastName, Gender.Unspecified, null, "");
        var hashPassword = passwordHasher.HashPassword(adminUser, settings.Password);
        
        adminUser.SetPasswordHash(hashPassword);
        adminUser.SetRole(ApplicationRole.SystemAdministrator);
        
        return adminUser;
    }
}
