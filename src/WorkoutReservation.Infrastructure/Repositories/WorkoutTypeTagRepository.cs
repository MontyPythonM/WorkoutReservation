using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class WorkoutTypeTagRepository : IWorkoutTypeTagRepository
{    
    private readonly AppDbContext _dbContext;

    public WorkoutTypeTagRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<WorkoutTypeTag>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.WorkoutTypeTags.AsQueryable();
        if (asNoTracking)
            query.AsNoTracking();
        
        return await query.ToListAsync(token);
    }

    public async Task<List<WorkoutTypeTag>> GetAllAsync(Expression<Func<WorkoutTypeTag, bool>> wherePredicate, bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.WorkoutTypeTags.AsQueryable().Where(wherePredicate);
        if (asNoTracking)
            query.AsNoTracking();
        
        return await query.ToListAsync(token);
    }

    public async Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.WorkoutTypeTags;
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.FirstOrDefaultAsync(x => x.Id == workoutTypeTagId, token);
    }
    
    public async Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, 
        Expression<Func<WorkoutTypeTag, object>>[] includes, CancellationToken token)
    {
        var baseQuery = _dbContext.WorkoutTypeTags.AsQueryable();
                
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                baseQuery = baseQuery.Include(include);
            }
        }
        
        return await baseQuery.FirstOrDefaultAsync(x => x.Id == workoutTypeTagId, token);
    }

    public async Task<WorkoutTypeTag> AddAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token)
    {
        await _dbContext.AddAsync(workoutTypeTag, token);
        await _dbContext.SaveChangesAsync(token);

        return workoutTypeTag;
    }

    public async Task DeleteAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token)
    {
        _dbContext.Remove(workoutTypeTag);
        await _dbContext.SaveChangesAsync(token);
    }
    
    public async Task UpdateAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token)
    {
        _dbContext.Update(workoutTypeTag);
        await _dbContext.SaveChangesAsync(token);
    }
}