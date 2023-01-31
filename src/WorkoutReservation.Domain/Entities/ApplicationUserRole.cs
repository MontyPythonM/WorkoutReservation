namespace WorkoutReservation.Domain.Entities;

public class ApplicationUserRole
{
    public Guid ApplicationUserId { get; set; }
    public int ApplicationRoleId { get; set; }
}