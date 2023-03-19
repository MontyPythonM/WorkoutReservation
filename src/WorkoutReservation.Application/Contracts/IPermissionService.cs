namespace WorkoutReservation.Application.Contracts;

public interface IPermissionService
{
    public Task<HashSet<string>> GetUserPermissionsAsync(Guid? userId, CancellationToken token);
    public Task<IEnumerable<string>> GetUserRolesAsync(Guid? userId, CancellationToken token);
}