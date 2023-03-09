using System.Security.Claims;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface ICurrentUserAccessor
{
    public Task<ApplicationUser> GetUserAsync(CancellationToken token);
    public Guid GetUserId();
    public IEnumerable<Claim> GetUserClaims();
    public HashSet<string> GetUserPermissions();
    public bool IsUserContextExist();
}