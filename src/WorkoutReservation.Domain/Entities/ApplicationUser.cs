using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string PasswordHash { get; private set; }
    public DateTime AccountCreationDate { get; private set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<ApplicationRole> ApplicationRoles { get; private set; } = new List<ApplicationRole>();

    protected ApplicationUser()
    {
        // for EF core
    }

    public ApplicationUser(string email, string firstName, string lastName, Gender? gender, DateOnly? dateOfBirth, string passwordHash)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PasswordHash = passwordHash;
        AccountCreationDate = DateTime.Now;
    }

    public void SetRole(ApplicationRole role) => ApplicationRoles.Add(role);
    public void SetPasswordHash(string passwordHash) => PasswordHash = passwordHash;
}
