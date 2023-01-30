using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IApplicationUserRepository
{
    public Task AddAsync(ApplicationUser user, CancellationToken token);
    public Task<ApplicationUser> GetByEmailAsync(string email, CancellationToken token);
    public Task<ApplicationUser> GetByGuidAsync(Guid guid, CancellationToken token);
    public Task<List<ApplicationUser>> GetAllAsync(CancellationToken token);
    public Task UpdateAsync(ApplicationUser user, CancellationToken token);
    public Task DeleteAsync(ApplicationUser user, CancellationToken token);
    public IQueryable<ApplicationUser> GetAllUsersQuery();
}
