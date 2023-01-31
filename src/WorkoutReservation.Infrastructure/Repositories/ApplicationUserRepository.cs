using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IRepository<ApplicationUser> _repository;

    public ApplicationUserRepository(AppDbContext dbContext, IRepository<ApplicationUser> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }
    
    public async Task<ApplicationUser> GetByEmailAsync(string email, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<ApplicationUser, object>>[] includes)
    {
        var query = _dbContext.ApplicationUsers.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(user => user.Email == email, token);
    }
    
    public async Task<ApplicationUser> GetByGuidAsync(Guid guid, bool asNoTracking = false, 
        CancellationToken token = default, params Expression<Func<ApplicationUser, object>>[] includes)
    { 
        var query = _dbContext.ApplicationUsers.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);
        
        return await query.FirstOrDefaultAsync(x => x.Id == guid, token);
    }

    public async Task<List<ApplicationUser>> GetAllAsync(bool asNoTracking = false, CancellationToken token = default,
        params Expression<Func<ApplicationUser, object>>[] includes)
    {
        var query = _dbContext.ApplicationUsers.AsQueryable();
        query = _repository.ApplyAsNoTracking(asNoTracking, query);
        query = _repository.ApplyIncludes(includes, query);     
                
        return await query.ToListAsync(token);
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
