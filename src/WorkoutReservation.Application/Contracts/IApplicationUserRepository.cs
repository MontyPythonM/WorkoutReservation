using System.Linq.Expressions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IApplicationUserRepository
{
    public Task AddAsync(ApplicationUser user, CancellationToken token);
    public Task<ApplicationUser> GetByEmailAsync(string email, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<ApplicationUser, object>>[] includes);
    public Task<ApplicationUser> GetByGuidAsync(Guid guid, bool asNoTracking = false, CancellationToken token = default);
    public Task<List<ApplicationUser>> GetAllAsync(bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<ApplicationUser, object>>[] includes);
    public Task UpdateAsync(ApplicationUser user, CancellationToken token);
    public Task DeleteAsync(ApplicationUser user, CancellationToken token);
    public IQueryable<ApplicationUser> GetAllUsersQuery();
}
