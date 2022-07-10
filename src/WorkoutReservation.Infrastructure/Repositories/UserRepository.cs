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

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbContext.Users   
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<User> GetByGuidAsync(Guid guid)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == guid);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    { 
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}
