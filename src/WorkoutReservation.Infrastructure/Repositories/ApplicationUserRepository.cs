using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly AppDbContext _dbContext;

    public ApplicationUserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser> GetByEmailAsync(string email, CancellationToken token)
    {
        return await _dbContext.ApplicationUsers   
            .Include(user => user.ApplicationRoles)
            .FirstOrDefaultAsync(user => user.Email == email, token);
    }
    public async Task<ApplicationUser> GetByGuidAsync(Guid guid, CancellationToken token)
    {
        return await _dbContext.ApplicationUsers
            .Include(x => x.ApplicationRoles)
            .FirstOrDefaultAsync(x => x.Id == guid, token);
    }

    public async Task<List<ApplicationUser>> GetAllAsync(CancellationToken token)
    {
        return await _dbContext.ApplicationUsers
            .AsNoTracking()
            .Include(x => x.ApplicationRoles)
            .ToListAsync(token);
    }

    public async Task AddAsync(ApplicationUser user, CancellationToken token)
    { 
        await _dbContext.ApplicationUsers.AddAsync(user, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(ApplicationUser user, CancellationToken token)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(ApplicationUser user, CancellationToken token)
    {
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public IQueryable<ApplicationUser> GetAllUsersQuery()
    {
        return _dbContext.ApplicationUsers
            .AsNoTracking()
            .Include(x => x.ApplicationRoles)
            .AsQueryable();
    }
}
