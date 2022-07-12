using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IUserRepository
{
    public Task AddAsync(User user);
    public Task<User> GetByEmailAsync(string email);
    public Task<User> GetByGuidAsync(Guid guid);
    public Task<List<User>> GetAllAsync();
    public Task UpdateAsync(User user);
    public Task DeleteAsync(User user);
    public IQueryable<User> GetAllQueriesAsync();
}
