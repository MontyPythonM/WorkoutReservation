using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class DummyUsers
{
    internal static IEnumerable<User> GetUsers()
    {
        var users = new List<User>
        {
            new ()
            {
                Email = "dummy-member@email.com",
                FirstName = "dummy",
                LastName = "member",
                Gender = Gender.Male,
                AccountCreationDate = DateTime.Now,
                UserRole = UserRole.Member
            },

            new()
            {
                Email = "dummy-manager@email.com",
                FirstName = "fake",
                LastName = "manager",
                Gender = Gender.Female,
                AccountCreationDate = new DateTime(2022, 01, 01),
                UserRole = UserRole.Manager
            },

            new()
            {
                Email = "dummy-notConfirmMember@email.com",
                FirstName = "member",
                LastName = "notConfirm",
                Gender = Gender.Female,
                AccountCreationDate = new DateTime(2022, 06, 06),
                UserRole = UserRole.NotConfirmedMember
            },
        };
        return users;
    }
}
