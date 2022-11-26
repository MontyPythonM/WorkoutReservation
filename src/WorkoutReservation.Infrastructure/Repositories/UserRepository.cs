using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken token)
    {
        return await _dbContext.Users   
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, token);
    }
    public async Task<User> GetByGuidAsync(Guid guid, CancellationToken token)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == guid, token);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken token)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync(token);
    }

    public async Task AddAsync(User user, CancellationToken token)
    { 
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(User user, CancellationToken token)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(User user, CancellationToken token)
    {
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public IQueryable<User> GetAllUsersQuery()
    {
        return _dbContext.Users
            .AsNoTracking()
            .AsQueryable();
    }
}
