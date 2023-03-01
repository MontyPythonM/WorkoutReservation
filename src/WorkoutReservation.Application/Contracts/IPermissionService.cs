namespace WorkoutReservation.Application.Contracts;

public interface IPermissionService
{
    Task<HashSet<string>> GetUserPermissionsAsync(Guid? userId, CancellationToken token);
    Task<IEnumerable<string>> GetUserRolesAsync(Guid? userId, CancellationToken token);
}