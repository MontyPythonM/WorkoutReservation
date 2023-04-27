using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Contracts;

public interface IApplicationUserRepository
{
    public Task AddAsync(ApplicationUser user, CancellationToken token);
    public Task<ApplicationUser> GetByEmailAsync(string email, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<ApplicationUser, object>>[] includes);
    public Task<ApplicationUser> GetByGuidAsync(Guid guid, bool asNoTracking = false, CancellationToken token = default);
    public Task UpdateAsync(ApplicationUser user, CancellationToken token);
    public Task<bool> IsEmailAlreadyTaken(string email, CancellationToken token);
    public Task<(List<ApplicationUser> users, int totalItems)> GetPagedAsync(IPagedQuery request,
        CancellationToken token);
    public Task<List<ApplicationUser>> GetByRoleAsync(ApplicationRole role, bool asNoTracking = false,
        CancellationToken token = default);
}
