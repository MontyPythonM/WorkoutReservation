using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Account.Queries.GetCurrentUser;

public class CurrentUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Permissions { get; set; }
}