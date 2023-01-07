using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class WorkoutTypeRepository : IWorkoutTypeRepository
{
    private readonly AppDbContext _dbContext;

    public WorkoutTypeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<WorkoutType>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        var query = _dbContext.WorkoutTypes.AsQueryable();
        if (asNoTracking)
            query.AsNoTracking();
        
        return await query.ToListAsync(token);
    }
    
    public IQueryable<WorkoutType> GetAllQuery()
    {
        return _dbContext.WorkoutTypes
            .AsNoTracking()
            .Include(x => x.Instructors)
            .Include(x => x.WorkoutTypeTags)
            .AsQueryable();
    }
    
    public async Task<WorkoutType> GetByIdAsync(int workoutTypeId, bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.WorkoutTypes.AsQueryable();
                
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.FirstOrDefaultAsync(x => x.Id == workoutTypeId, token);
    }
    
    public async Task<WorkoutType> GetByIdAsync(int workoutTypeId, bool asNoTracking = false, CancellationToken token = default, params Expression<Func<WorkoutType, object>>[] includes)
    {
        var baseQuery = _dbContext.WorkoutTypes.AsQueryable();
                
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                baseQuery = baseQuery.Include(include);
            }
        }
        
        return await baseQuery.FirstOrDefaultAsync(x => x.Id == workoutTypeId, token);
    }
    
    public async Task<WorkoutType> AddAsync(WorkoutType workoutType, CancellationToken token)
    {
        await _dbContext.AddAsync(workoutType, token);
        await _dbContext.SaveChangesAsync(token);

        return workoutType;
    }

    public async Task DeleteAsync(WorkoutType workoutType, CancellationToken token)
    {
        _dbContext.Remove(workoutType);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(List<WorkoutType> workoutTypes, CancellationToken token)
    {
        _dbContext.RemoveRange(workoutTypes);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(WorkoutType workoutType, CancellationToken token)
    {
        _dbContext.Update(workoutType);
        await _dbContext.SaveChangesAsync(token);
    }
}
