using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class WorkoutTypeTagRepository : IWorkoutTypeTagRepository
{    
    private readonly AppDbContext _dbContext;

    public WorkoutTypeTagRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<WorkoutTypeTag>> GetAllAsync(bool asNoTracking, CancellationToken token, 
        params Expression<Func<WorkoutTypeTag, object>>[] includes)
    {
        return await _dbContext.WorkoutTypeTags
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .ToListAsync(token);
    }

    public async Task<List<WorkoutTypeTag>> GetAllAsync(Expression<Func<WorkoutTypeTag, bool>> wherePredicate, 
        bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.WorkoutTypeTags
            .Where(wherePredicate)
            .ApplyAsNoTracking(asNoTracking)
            .ToListAsync(token);
    }

    public async Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, CancellationToken token)
    {
        return await _dbContext.WorkoutTypeTags
            .ApplyAsNoTracking(asNoTracking)
            .FirstOrDefaultAsync(tag => tag.Id == workoutTypeTagId, token);
    }
    
    public async Task<WorkoutTypeTag> GetByIdAsync(int workoutTypeTagId, bool asNoTracking, 
        CancellationToken token, params Expression<Func<WorkoutTypeTag, object>>[] includes)
    {
        return await _dbContext.WorkoutTypeTags
            .ApplyAsNoTracking(asNoTracking)
            .ApplyIncludes(includes)
            .FirstOrDefaultAsync(tag => tag.Id == workoutTypeTagId, token);
    }

    public async Task AddAsync(WorkoutTypeTag workoutTypeTag, CancellationToken token)
    {
        await _dbContext.AddAsync(workoutTypeTag, token);
        await _dbContext.SaveChangesAsync(token);
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