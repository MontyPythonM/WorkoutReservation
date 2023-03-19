using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities;

public sealed class ApplicationRole : Enumeration<ApplicationRole>
{
    public ICollection<ApplicationPermission> ApplicationPermissions { get; } = new List<ApplicationPermission>();
    public ICollection<ApplicationUser> ApplicationUsers { get; } = new List<ApplicationUser>();
    
    public static readonly ApplicationRole SystemAdministrator = new((int)Role.SystemAdministrator, nameof(SystemAdministrator));
    public static readonly ApplicationRole BusinessAdministrator = new((int)Role.BusinessAdministrator, nameof(BusinessAdministrator));
    public static readonly ApplicationRole Manager = new((int)Role.Manager, nameof(Manager));
    public static readonly ApplicationRole Member = new((int)Role.Member, nameof(Member));

    private ApplicationRole()
    {
        // required by EF core
    }
    
    private ApplicationRole(int id, string name)
        : base(id, name)
    {
    }
}