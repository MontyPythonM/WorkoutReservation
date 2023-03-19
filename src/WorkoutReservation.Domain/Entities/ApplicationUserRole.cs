namespace WorkoutReservation.Domain.Entities;

public class ApplicationUserRole
{
    public Guid ApplicationUserId { get; }
    public int ApplicationRoleId { get; }

    private ApplicationUserRole()
    {
        // required by EF core
    }

    public ApplicationUserRole(Guid applicationUserId, int applicationRoleId)
    {
        ApplicationUserId = applicationUserId;
        ApplicationRoleId = applicationRoleId;
    }
}