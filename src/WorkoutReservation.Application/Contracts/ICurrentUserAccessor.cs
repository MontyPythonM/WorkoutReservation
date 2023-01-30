using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface ICurrentUserAccessor
{
    public Task<ApplicationUser> GetCurrentUserAsync(CancellationToken token);
    public Guid GetCurrentUserId();
}