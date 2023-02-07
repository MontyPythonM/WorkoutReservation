namespace WorkoutReservation.Infrastructure.Interfaces;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid? userId, CancellationToken token);
}