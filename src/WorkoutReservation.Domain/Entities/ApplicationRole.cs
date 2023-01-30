using WorkoutReservation.Domain.Abstractions;

namespace WorkoutReservation.Domain.Entities;

public sealed class ApplicationRole : Enumeration<ApplicationRole>
{
    public ICollection<ApplicationPermission> ApplicationPermissions { get; } = new List<ApplicationPermission>();
    public ICollection<ApplicationUser> ApplicationUsers { get; } = new List<ApplicationUser>();
    
    public static readonly ApplicationRole SystemAdministrator = new(1, nameof(SystemAdministrator));
    public static readonly ApplicationRole BusinessAdministrator = new(2, nameof(BusinessAdministrator));
    public static readonly ApplicationRole Manager = new(3, nameof(Manager));
    public static readonly ApplicationRole Member = new(4, nameof(Member));

    public ApplicationRole()
    {
    }
    
    private ApplicationRole(int id, string name)
        : base(id, name)
    {
    }
}