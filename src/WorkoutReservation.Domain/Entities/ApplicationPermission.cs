using WorkoutReservation.Domain.Abstractions;

namespace WorkoutReservation.Domain.Entities;

public sealed class ApplicationPermission : Enumeration<ApplicationRole>
{
    public ICollection<ApplicationRole> ApplicationRoles { get; } = new List<ApplicationRole>();

    private ApplicationPermission()
    {
        // required by EF core
    }

    public ApplicationPermission(int id, string name) 
        : base(id, name)
    {
    }
}