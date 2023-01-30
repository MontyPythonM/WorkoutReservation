using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class RealWorkoutRepository : IRealWorkoutRepository
{
    private readonly AppDbContext _dbContext;

    public RealWorkoutRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RealWorkout>> GetAllAsync(bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.RealWorkouts.AsQueryable();
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.ToListAsync(token);
    }
    
    public async Task<List<RealWorkout>> GetAllAsync(Expression<Func<RealWorkout, bool>> wherePredicate, 
        bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.RealWorkouts.AsQueryable().Where(wherePredicate);
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.ToListAsync(token);
    }
    
    public async Task<List<RealWorkout>> GetByDayAsync(DateOnly day, bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.RealWorkouts
            .Where(x => x.Date == day)
            .OrderBy(x => x.StartTime)
            .AsQueryable();
        
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.ToListAsync(token);
    }
    
    public async Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate, 
        DateOnly endDate, bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.RealWorkouts
            .Where(x => x.Date >= startDate && x.Date <= endDate)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .AsQueryable();
        
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.ToListAsync(token);
    }

    public async Task<List<RealWorkout>> GetAllFromDateRangeAsync(DateOnly startDate, DateOnly endDate, 
        bool asNoTracking, CancellationToken token, params Expression<Func<RealWorkout, object>>[] includes)
    {
        var baseQuery = _dbContext.RealWorkouts
            .Where(x => x.Date >= startDate && x.Date <= endDate)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .AsQueryable();
        
        if (asNoTracking)
            baseQuery.AsNoTracking();

        if (includes.Any())
        {
            foreach (var include in includes)
            {
                baseQuery = baseQuery.Include(include);
            }
        }
        
        return await baseQuery.ToListAsync(token);
    }
    
    public async Task<RealWorkout> GetByIdAsync(int realWorkoutId, bool asNoTracking, CancellationToken token)
    {
        var baseQuery = _dbContext.RealWorkouts.AsQueryable();
         
        if (asNoTracking)
            baseQuery.AsNoTracking();
        
        return await baseQuery.FirstOrDefaultAsync(x => x.Id == realWorkoutId, token);             
    }
    
    public async Task<RealWorkout> GetByIdAsync(int realWorkoutId, bool asNoTracking, CancellationToken token, 
        params Expression<Func<RealWorkout, object>>[] includes)
    {
         var baseQuery = _dbContext.RealWorkouts.AsQueryable();
         
         if (asNoTracking)
             baseQuery.AsNoTracking();
             
         if (includes.Any())
         {
             foreach (var include in includes)
             {
                 baseQuery = baseQuery.Include(include);
             }
         }
         
         return await baseQuery.FirstOrDefaultAsync(x => x.Id == realWorkoutId, token);             
    }
    
    public async Task<RealWorkout> AddAsync(RealWorkout realWorkout, CancellationToken token)
    {
        await _dbContext.AddAsync(realWorkout, token);
        await _dbContext.SaveChangesAsync(token);
        return realWorkout;
    }
    
    public async Task AddAsync(List<RealWorkout> realWorkouts, CancellationToken token)
    {
        await _dbContext.AddRangeAsync(realWorkouts, token);
        await _dbContext.SaveChangesAsync(token);
    }
    
    public async Task DeleteAsync(RealWorkout realWorkout, CancellationToken token)
    {
        _dbContext.Remove(realWorkout);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(RealWorkout realWorkout, CancellationToken token)
    {
        _dbContext.Update(realWorkout);
        await _dbContext.SaveChangesAsync(token);
    }
}
