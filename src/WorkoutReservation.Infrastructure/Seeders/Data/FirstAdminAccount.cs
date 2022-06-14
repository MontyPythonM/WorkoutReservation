using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data
{
    internal static class FirstAdminAccount
    {
        internal static User GerFirstAdmin(FirstAdminSettings firstAdminSettings)
        {
            var admin = new User()
            {
                Email = firstAdminSettings.Email,
                FirstName = firstAdminSettings.FirstName,
                LastName = firstAdminSettings.LastName,
                Gender = null,
                UserRole = UserRole.administrator,
                AccountCreationDate = DateTime.Now,
                PasswordHash = firstAdminSettings.Password
            };

            return admin;
        }
    }
}
