using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data
{
    internal static class FirstAdminAccount
    {
        internal static User GerFirstAdmin(FirstAdminSettings firstAdmin, 
                                           IPasswordHasher<User> passwordHasher)
        {

            var admin = new User()
            {
                Email = firstAdmin.Email,
                FirstName = firstAdmin.FirstName,
                LastName = firstAdmin.LastName,
                Gender = null,
                UserRole = UserRole.Administrator,
                AccountCreationDate = DateTime.Now,
            };

            var hashPassword = passwordHasher.HashPassword(admin, firstAdmin.Password);
            admin.PasswordHash = hashPassword;

            return admin;
        }
    }
}
