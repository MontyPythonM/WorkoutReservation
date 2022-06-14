using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data
{
    internal class DummyUsers
    {
        internal static List<User> GetUsers()
        {
            var users = new List<User>
            {
                new User()
                {
                    Email = "dummy-member@email.com",
                    FirstName = "dummy",
                    LastName = "member",
                    Gender = Gender.male,
                    AccountCreationDate = DateTime.Now,
                    UserRole = UserRole.member
                },

                new User()
                {
                    Email = "dummy-manager@email.com",
                    FirstName = "fake",
                    LastName = "manager",
                    Gender = Gender.female,
                    AccountCreationDate = new DateTime(2022, 01, 01),
                    UserRole = UserRole.manager
                },

                new User()
                {
                    Email = "dummy-notConfirmMember@email.com",
                    FirstName = "member",
                    LastName = "notConfirm",
                    Gender = Gender.female,
                    AccountCreationDate = new DateTime(2022, 06, 06),
                    UserRole = UserRole.notConfirmedMember
                },
            };

            return users;
        }
    }
}
