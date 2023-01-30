using WorkoutReservation.Domain.Abstractions;

namespace WorkoutReservation.Domain.Entities;

public class ApplicationPermission : Enumeration<ApplicationRole>
{
    public ICollection<ApplicationRole> ApplicationRoles { get; } = new List<ApplicationRole>();
    
    protected ApplicationPermission()
    {
        // for EF core
    }
    
    public ApplicationPermission(int id, string name) 
        : base(id, name)
    {
    }
}