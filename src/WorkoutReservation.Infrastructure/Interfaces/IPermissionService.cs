using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Interfaces;

public interface IPermissionService
{
    Task<HashSet<string>> GetUserPermissionsAsync(Guid? userId, CancellationToken token);
    Task<IEnumerable<string>> GetUserRolesAsync(Guid? userId, CancellationToken token);
}