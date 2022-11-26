using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IUserRepository
{
    public Task AddAsync(User user, CancellationToken token);
    public Task<User> GetByEmailAsync(string email, CancellationToken token);
    public Task<User> GetByGuidAsync(Guid guid, CancellationToken token);
    public Task<List<User>> GetAllAsync(CancellationToken token);
    public Task UpdateAsync(User user, CancellationToken token);
    public Task DeleteAsync(User user, CancellationToken token);
    public IQueryable<User> GetAllUsersQuery();
}
